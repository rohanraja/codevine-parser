using System;
using System.Collections.Generic;
using System.IO;

namespace CodeReader
{
    public class DataReader
    {

		public string ProjectPath;

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
