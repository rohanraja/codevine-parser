using System;
using CodeReader;

namespace CodePraser
{
    public class SourceFile
    {
		public string FPath { get; }
        public string RootDir { get; }

		private DataReader dataReader;

		public SourceFile(string fPath, string rootDir)
        {
			FPath = fPath;
			RootDir = rootDir;
			dataReader = new DataReader(RootDir);
		}

        public string GetCode()
		{
			return dataReader.GetContentsOfFileAtRoot(FPath);
		}

        public void UpdateCodeContents(string newContents)
		{
			dataReader.SetContentsOfFileAtRoot(FPath, newContents);

		}


	}
}
