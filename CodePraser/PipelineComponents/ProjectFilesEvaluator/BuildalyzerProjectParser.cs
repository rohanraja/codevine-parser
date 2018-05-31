using System;
using System.Collections.Generic;
using System.IO;
using CodeParserCommon;

namespace CodePraser
{
    public class BuildalyzerProjectParser : IProjectParser
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ICSFilesLister cSFilesLister = null;

		public BuildalyzerProjectParser()
		{
            cSFilesLister = new CRM_Solutions_CSFileLister_Decorator();
		}

		public SourceCodeInfo GetSourceCodeInfo(string rootDir, string proName)
		{

			log.Debug(new { rootDir, proName });

            var sourceCodeInfo = new SourceCodeInfo(rootDir);

			var fullProjPath = Path.Combine(rootDir, proName);

            List<string> csFiles = new List<string>() { };

            try
            {
                log.Debug("Attempting to analyze CSPROJ and fetch code files");
                csFiles = cSFilesLister.GetCSCodeFiles(fullProjPath);
                log.InfoFormat("Found {0} CS Code files in this project", csFiles.Count);
                log.Debug(csFiles.ToArray()) ;
            }
            catch(Exception e)
            {
                log.Error("Error in analyzing CSPROJ", e);
            }

            foreach (var item in csFiles)
            {
                sourceCodeInfo.AddCodeFile(item);
            }

			return sourceCodeInfo;

		}


	}
}
