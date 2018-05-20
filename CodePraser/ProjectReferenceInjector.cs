using System;
using CodeReader;

namespace CodePraser
{
	public class ProjectReferenceInjector : IProjectReferenceInjector
	{
		public void InjectReference(SourceCodeInfo sourceCodeInfo, string projectFileName)
		{
			DataReader dataReader = new DataReader(sourceCodeInfo.BaseDirPath);

			var prdata = dataReader.GetContentsOfFileAtRoot(projectFileName);

			string prPath = @"/Users/rohan/code/codevine_parser/CodeVine_Parser/CodeRecordHelpers/CodeRecordHelpers.csproj";

			string replaceData = string.Format("<ItemGroup><ProjectReference Include=\"{0}\" /></ItemGroup></Project>", prPath);

			var replacedStr = prdata.Replace("</Project>", replaceData);

			dataReader.SetContentsOfFileAtRoot(projectFileName, replacedStr);
		}
	}
}