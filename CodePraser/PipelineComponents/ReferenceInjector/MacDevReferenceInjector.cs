using System;
using CodeParserCommon;

namespace CodePraser
{
	public class MacDevReferenceInjector : IProjectReferenceInjector
	{
        private static string CODEVINE_REFERENCE_STR = @"<ItemGroup><PackageReference Include=""CodeVineRecorder"" Version=""1.0.10"" /></ItemGroup></Project>";

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public void InjectReference(SourceCodeInfo sourceCodeInfo, string projectFileName)
        {
            string prdata = sourceCodeInfo.GetContentsOfFileAtRoot(projectFileName);

			string prPath = @"/Users/rohan/code/codevine-recorder/CodeVineRecorder/CodeVineRecorder.csproj";

            string replaceData = string.Format("<ItemGroup><ProjectReference Include=\"{0}\" /></ItemGroup></Project>", prPath);

			var replacedStr = prdata.Replace("</Project>", replaceData);

            sourceCodeInfo.SetContentsOfFileAtRoot(projectFileName, replacedStr);
        }

    }
}