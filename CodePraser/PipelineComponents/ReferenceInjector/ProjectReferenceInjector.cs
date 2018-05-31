using System;
using CodeParserCommon;

namespace CodePraser
{
	public class ProjectReferenceInjector : IProjectReferenceInjector
	{
        private static string CODEVINE_REFERENCE_STR = @"<ItemGroup><Reference Include=""Newtonsoft.Json""><SpecificVersion>False</SpecificVersion><Private>true</Private><HintPath>$(PKG_JSON)\lib\net40\Newtonsoft.Json.dll</HintPath></Reference><Reference Include=""StackExchange.Redis""><SpecificVersion>False</SpecificVersion><Private>true</Private><HintPath>$(PKG_REDIS)\lib\net45\StackExchange.Redis.dll</HintPath></Reference><Reference Include=""CodeVineRecorder""><SpecificVersion>False</SpecificVersion><Private>true</Private><HintPath>$(PKG_CODEVINE)\lib\net452\CodeVineRecorder.dll</HintPath></Reference></ItemGroup></Project>";

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public void InjectReference(SourceCodeInfo sourceCodeInfo, string projectFileName)
		{
			string prdata = sourceCodeInfo.GetContentsOfFileAtRoot(projectFileName);

            if (prdata.Contains("CodeVineRecorder.dll"))
            {
                log.InfoFormat("Project {0} already contains the hooks, skipping injection of reference", projectFileName);
                return;
            }
            var replacedStr = ReplaceLastOccurrence(prdata, "</Project>", CODEVINE_REFERENCE_STR);

			sourceCodeInfo.SetContentsOfFileAtRoot(projectFileName, replacedStr);
		}

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}