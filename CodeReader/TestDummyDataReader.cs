using System;
namespace CodeReader
{
	public class TestDummyDataReader : DataReader
    {
		private static string testProjectPath = @"/Users/rohan/code/codevine_parser/CodeVine_Parser/TestCSharpProject";

		public TestDummyDataReader(string path) : base(path)
        {
        }

		public static TestDummyDataReader GetTestReader()
        {
			return new TestDummyDataReader(testProjectPath);

        }
    }
}
