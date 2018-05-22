using System;
using CodeParserCommon;
using CodeReader;
using CodeRecordHelpers;

namespace CodePraser
{
	public class CodeRegisterer : ICodeRegisterer
	{
		private CodeHooks codeHooks;

		public CodeRegisterer()
		{
			codeHooks = CodeHooks.Instance();
		}

		public CodeRegisterer(CodeHooks pCodeHooks)
        {
			codeHooks = pCodeHooks;
        }

		public void SendCodeContentsToServer(SourceCodeInfo sourceCode)
		{
			foreach(var sourceFile in sourceCode.SourceFiles)
			{
				RegisterFile(sourceFile);			
			}
		}

		private void RegisterFile(SourceFile sourceFile)
		{
			codeHooks.AddSourceFile(sourceFile.FilePath, sourceFile.GetCode());
		}
	}
}