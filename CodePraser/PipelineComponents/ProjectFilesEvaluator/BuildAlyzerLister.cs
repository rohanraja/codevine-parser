using System;
using System.Collections.Generic;
using System.Text;
using Buildalyzer;
using Microsoft.CodeAnalysis;

namespace CodePraser
{
    public class BuildAlyzerLister : ICSFilesLister
    {
        public List<string> GetCSCodeFiles(string projPath)
        {
            AnalyzerManager manager = new AnalyzerManager();
            var pro = manager.GetProject(projPath);
            var pro2 = pro.Load();

            List<string> outP = new List<string>() { };

            foreach (var item in pro2.Items)
            {
                if (item.ItemType == "Compile")
                {
                    outP.Add(item.EvaluatedInclude);
                }
            }

            return outP;
        }
    }
}
