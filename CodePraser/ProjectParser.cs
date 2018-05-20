﻿using System;
using System.Collections.Generic;
using System.IO;
using Buildalyzer;
using Microsoft.CodeAnalysis;

namespace CodePraser
{
    public class ProjectParser : IProjectParser
	{
		public ProjectParser()
		{
		}

		public SourceCodeInfo GetSourceCodeInfo(string rootDir, string proName)
		{
			
			var sln = Path.Combine(rootDir, proName);
            AnalyzerManager manager = new AnalyzerManager();
            var pro = manager.GetProject(sln);
            var pro2 = pro.Load();

            var sourceCodeInfo = new SourceCodeInfo(rootDir);

            foreach (var item in pro2.Items)
            {
                if (item.ItemType == "Compile")
                {

                    sourceCodeInfo.AddCodeFile(item.EvaluatedInclude);

                }
            }

			return sourceCodeInfo;

		}

	}
}
