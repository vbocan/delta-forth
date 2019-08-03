using System;
using System.Reflection;

namespace ForthLibrary
{
	/// <summary>
	/// Provides the basic stack operations and word calling for an associated Delta Forth .NET program.
	/// </summary>
	public class ForthExtender
	{
		/// <summary>
		/// Delta Forth .NET Assembly
		/// </summary>
		private Assembly	calAssembly;
		/// <summary>
		/// Delta Forth .NET Type
		/// </summary>
		private Type		calType;

		private int []ForthStack;
		private int ForthStackIndex;

		/// <summary>
		/// Initializes the assembly and the stacks, including stack pointers
		/// </summary>
		/// <param name="FileName">File name containing the Delta Forth .NET program</param>
		public ForthExtender(string FileName)
		{
			// Initialize assembly and type
			calAssembly = Assembly.LoadFrom(FileName);
			calType = calAssembly.GetType("DeltaForthEngine", true, true);

			// Initialize Forth stack
			ForthStack = (int[])GetField("ForthStack");
			ForthStackIndex = (int)GetField("ForthStackIndex");
		}
		#region "Forth Stack Handling Methods"
		/// <summary>
		/// Pushes an integer value onto the Forth stack
		/// </summary>
		/// <param name="val">Value to push</param>
		public void Push(int val)
		{
			ForthStack[ForthStackIndex++] = val;
			FieldInfo fi = calType.GetField("ForthStackIndex");
			fi.SetValue(null, ForthStackIndex);
		}

		/// <summary>
		/// Pops an integer value from the Forth stack
		/// </summary>
		/// <returns>Value retrieved from the stack</returns>
		public int Pop()
		{
			FieldInfo fi = calType.GetField("ForthStackIndex");
			fi.SetValue(null, --ForthStackIndex);
			return ForthStack[ForthStackIndex];
		}

		/// <summary>
		/// Peeks the value on top of the Forth stack
		/// </summary>
		/// <returns>Value retrieved from the stack</returns>
		public int Peek()
		{
			return ForthStack[ForthStackIndex - 1];
		}

		#endregion
		/// <summary>
		/// Returns the object associated with the specified field (Delta Forth .NET variable)
		/// </summary>
		/// <param name="FieldName">Field name</param>
		/// <returns>Object associated with the field</returns>
		private object GetField(string FieldName)
		{
			FieldInfo fi = calType.GetField(FieldName);
			if(fi == null) 
			{
				return null;
			}
			return fi.GetValue(null);
		}
		
		/// <summary>
		/// Calls a specified Delta Forth .NET word
		/// </summary>
		/// <param name="WordName">Word name</param>
		/// <returns>TRUE if the call is successful</returns>
		private bool CallForthWord(string WordName)
		{
			MethodInfo calMethodInfo = calType.GetMethod(WordName.ToUpper());
			if(calMethodInfo == null) 
			{
				return false;
			}
			calMethodInfo.Invoke(null, null);
			return true;
		}

	}
}
