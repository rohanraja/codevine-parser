using System;
using System.Collections.Generic;

namespace CodePraser
{
    public class SourceCodeInfo
    {
		public string BaseDirPath;
		public List<string> CodeFiles = new List<string>() { };

        public SourceCodeInfo(string baseDirPath)
        {
            this.BaseDirPath = baseDirPath;
		}

        public void AddCodeFile(string relativePath)
		{
			CodeFiles.Add(relativePath);
		}
    }
}
