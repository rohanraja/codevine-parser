using System;

namespace CodeReader
{
    class Program
    {
        static void Main(string[] args)
        {
			var dataReader = TestDummyDataReader.GetTestReader();
			string outp = dataReader.GetMainFile();

            Console.WriteLine(outp);
        }
    }
}
