using System;
using System.IO;

namespace CodeReader
{
    public class DataReader
    {

		private static string testProjectPath = @"/Users/rohan/code/codevine_parser/CodeVine_Parser/TestCSharpProject";


		public string ProjectPath;

		public static DataReader GetTestReader()
		{
			return new DataReader(testProjectPath);
			
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

		public DataReader(string projectPath)
		{
			ProjectPath = projectPath;
		}

    }
}
