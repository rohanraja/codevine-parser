using System;
using System.Collections.Generic;
using System.IO;
using Buildalyzer;
using Microsoft.CodeAnalysis;
using CodeParserCommon;

namespace CodePraser
{
    public class BuildalyzerProjectParser : IProjectParser
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public BuildalyzerProjectParser()
		{
		}

		public SourceCodeInfo GetSourceCodeInfo(string rootDir, string proName)
		{

			log.Debug(new { rootDir, proName });

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
