using System;
using System.Collections.Generic;

namespace CodeParserCommon
{
    public class SourceCodeInfo
	{
		public string BaseDirPath;
		private DataReader dataReader;
		public List<string> CodeFiles = new List<string>() { };

		public List<SourceFile> SourceFiles = new List<SourceFile>() { };

        public SourceCodeInfo(string baseDirPath)
        {
            this.BaseDirPath = baseDirPath;
			dataReader = new DataReader(baseDirPath);
		}

		public void AddCodeFile(string relativePath)
		{
			CodeFiles.Add(relativePath);

			SourceFiles.Add(new SourceFile(relativePath, BaseDirPath));
		}

		public void SetContentsOfFileAtRoot(string projectFileName, string replacedStr)
		{
			dataReader.SetContentsOfFileAtRoot(projectFileName, replacedStr);
		}

		public string GetContentsOfFileAtRoot(string projectFileName)
        {
			return dataReader.GetContentsOfFileAtRoot(projectFileName);
        }

		public List<string> ListCodeFiles()
		{
			throw new NotImplementedException();
		}

		public string GetProjectFile()
		{
			throw new NotImplementedException();
		}
	}
}
