using System;
using System.Collections.Generic;
using CodePraser.HooksInjection;

namespace CodePraser
{
    public class SourceCodeInfo
    {
		public string BaseDirPath;
		public List<string> CodeFiles = new List<string>() { };

		public List<SourceFile> SourceFiles = new List<SourceFile>() { };

        public SourceCodeInfo(string baseDirPath)
        {
            this.BaseDirPath = baseDirPath;
		}

        public void AddCodeFile(string relativePath)
		{
			CodeFiles.Add(relativePath);

			SourceFiles.Add(new SourceFile(relativePath, BaseDirPath));
		}
    }
}
