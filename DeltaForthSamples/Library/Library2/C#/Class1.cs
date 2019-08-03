using System;
using System.Reflection;
using System.IO;

namespace Library2
{
	class Class1
	{
		static Assembly calAssembly;
		static Type calType;
		
		// Forth stack & index
		static int []ForthStack;
		static int ForthStackIndex;

		[STAThread]
		static void Main(string[] args)
		{
            if (!InitDLL("ForthLibrary.dll", "ForthLib")) return;            
			Push(30);
			Push(18);
			CallForthWord("ADDITION");
			Console.WriteLine("Addition...");
			CallForthWord("DISPLAYTOPOFSTACK");
			Push(30);
			Push(18);
			CallForthWord("SUBTRACTION");
			Console.WriteLine("Subtraction...");
			CallForthWord("DISPLAYTOPOFSTACK");
		}

		// GetField - gets the value of a field defined in the Forth library
		static object GetField(string FieldName)
		{
			FieldInfo fi = calType.GetField(FieldName);
			if(fi == null) 
			{
				Console.WriteLine("Runtime Error: Could not find field {0}.", FieldName);
				return null;
			}
			return fi.GetValue(null);
		}

		// InitDLL - Loads the Forth library and sets up the stack pointers
		static private bool InitDLL(string FileName, string ClassName)
		{
			try 
			{
				calAssembly = Assembly.LoadFrom(FileName);
			} 
			catch (FileLoadException fle)
			{
				Console.WriteLine("Error: {0}", fle.Message);
				return false;
			}
            try
            {
                calType = calAssembly.GetType(ClassName, true, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Runtime Error: Could not load class {0} from library. Reason: {1}", ClassName, ex.Message);
                return false;
            }

			CallForthWord("MAIN");

			// Initialize stacks and indexes
			ForthStack = (int[])GetField("ForthStack");
			ForthStackIndex = (int)GetField("ForthStackIndex");
			return true;
		}

		// ShutdownDLL - Unloads the library
		static private void ShutdownDLL()
		{
			calAssembly = null;
		}

		// CallForthWord - Calls the specified Forth word
		static private bool CallForthWord(string WordName)
		{
			MethodInfo calMethodInfo = calType.GetMethod(WordName);
			if(calMethodInfo == null) 
			{
				Console.WriteLine("Runtime Error: Could not find word {0}.", WordName);
				return false;
			}
			calMethodInfo.Invoke(null, null);
			return true;
		}

		
		// Push - pushes an integer value onto the Forth stack
		static private void Push(int val)
		{
			ForthStack[ForthStackIndex++] = val;
			FieldInfo fi = calType.GetField("ForthStackIndex");
			fi.SetValue(null, ForthStackIndex);
		}

		// Pop - pops an integer value from the Forth stack
		static private int Pop()
		{
			FieldInfo fi = calType.GetField("ForthStackIndex");
			fi.SetValue(null, --ForthStackIndex);
			return ForthStack[ForthStackIndex];
		}

		// Peek - peeks the value on top of stack
		static private int Peek()
		{
			return ForthStack[ForthStackIndex - 1];
		}

	}
}
