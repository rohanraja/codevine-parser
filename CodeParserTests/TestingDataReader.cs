using System;
using System.IO;

namespace CodeParserTests
{
    public class TestingDataReader
    {

		private static string testProjectPath = @"/Users/rohan/code/codevine_parser/CodeVine_Parser/TestCSharpProject";


		public string ProjectPath;

		public static TestingDataReader GetReader()
		{
			return new TestingDataReader(testProjectPath);
			
		}

        public string GetMainFile()
		{
			string mainProgPath = GetFullPath("Program.cs");
			return File.ReadAllText(mainProgPath);
		}

		private string GetFullPath(string fname)
		{
			string fpath = ProjectPath + "/" + fname;
			return fpath;

		}

		public TestingDataReader(string projectPath)
		{
			ProjectPath = projectPath;
		}

    }
}
