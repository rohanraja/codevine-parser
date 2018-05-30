using System;

namespace TestCSharpProject
{
    class Program
    {
        static void Main(string[] args)
        {
			ClassA classA = new ClassA();
			Console.WriteLine("FROM MAIN: " + classA.GetHashCode());
			int cnt = classA.GetCount();
			Console.WriteLine("Count is: " + cnt.ToString());
			classA.MethodA_1();
			cnt = classA.GetCount();
			Console.WriteLine("Count is: " + cnt.ToString());

            if(cnt == 900)
			{
				Console.WriteLine("");
				Console.WriteLine("");
			}
			else
			{
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("");

			}
        }
    }
}
