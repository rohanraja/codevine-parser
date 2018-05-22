using System;
using CodeReader;
using CodeParserCommon;

namespace CodePraser
{
	public class ProjectReferenceInjector : IProjectReferenceInjector
	{
		public void InjectReference(SourceCodeInfo sourceCodeInfo, string projectFileName)
		{
			string prdata = sourceCodeInfo.GetContentsOfFileAtRoot(projectFileName);

			string prPath = @"/Users/rohan/code/codevine_parser/CodeVine_Parser/CodeRecordHelpers/CodeRecordHelpers.csproj";

			string replaceData = string.Format("<ItemGroup><ProjectReference Include=\"{0}\" /></ItemGroup></Project>", prPath);

			var replacedStr = prdata.Replace("</Project>", replaceData);

			sourceCodeInfo.SetContentsOfFileAtRoot(projectFileName, replacedStr);
		}
	}
}