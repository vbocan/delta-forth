using System;

namespace Library1
{
	public class Class2
	{
		public Class2()
		{
			Console.WriteLine("This is the constructor of the Class2 class.");
		}

		public void DisplayRandom()
		{
			Random RandNumber = new Random();
			Console.WriteLine("Random number: {0}", RandNumber.Next(1000));
			RandNumber = null;
		}
	}
}
