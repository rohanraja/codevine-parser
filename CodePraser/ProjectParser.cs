using System;
using System.Collections.Generic;
using System.IO;
using Buildalyzer;
using Microsoft.CodeAnalysis;

namespace CodePraser
{
    public class ProjectParser
    {
		public string RootDir { get; }
		public string projName { get; }
		public SourceCodeInfo sourceCodeInfo { get; private set; }

		public ProjectParser(string rootDir, string proName)
		{
			RootDir = rootDir;
			projName = proName;
			Parse();
		}

		public void Parse()
		{
			var sln = Path.Combine(RootDir, projName);
			AnalyzerManager manager = new AnalyzerManager();
			var pro = manager.GetProject(sln);
			var pro2 = pro.Load();

			sourceCodeInfo = new SourceCodeInfo(RootDir);

			foreach(var item in pro2.Items)
			{
				if(item.ItemType == "Compile"){

					sourceCodeInfo.AddCodeFile(item.EvaluatedInclude);

				}
			}


		}
	}
}
