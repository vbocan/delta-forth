using System;
using System.Threading;

namespace ForthLibrary
{
	public class ForthLibrary
	{
		private static ForthExtender fe;

		static ForthLibrary()
		{
			Console.WriteLine("ForthLibrary constructor called...");
			fe = new ForthExtender("ForthLibraryTest.exe");
		}

		public static void GenerateRandomNumber(string param)
		{
			Console.WriteLine("Received parameter: " + param);
			Thread.Sleep(50);
			Random RandNumber = new Random();
			int number = RandNumber.Next(50000);
			RandNumber = null;
			fe.Push(number);
		}
	}
}
