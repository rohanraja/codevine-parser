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
			return GetContentsOfFileAtRoot("Program.cs");
		}

        public string GetContentsOfFileAtRoot(string fname)
		{
			string fPath = GetFullPath(fname);
			return File.ReadAllText(fPath);

		}

		public string GetFullPath(string fname)
		{
			string fpath = ProjectPath + "/" + fname;
			return fpath;

		}

		public DataReader(string projectPath)
		{
			ProjectPath = projectPath;
		}

        public string GetCSProjContents()
		{
			string fname = GetCsProjFname();
			return GetContentsOfFileAtRoot(fname);
		}

		public virtual string GetCsProjFname()
		{
			throw new NotImplementedException() ;
		}
    }
}
