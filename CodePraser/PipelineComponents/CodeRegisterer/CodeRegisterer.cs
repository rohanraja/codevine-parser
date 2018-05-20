using System;
using CodePraser.HooksInjection;
using CodeReader;
using CodeRecordHelpers;

namespace CodePraser
{
	public class CodeRegisterer : ICodeRegisterer
	{
		private DataReader dataReader;
		private CodeHooks codeHooks;

		public CodeRegisterer()
		{
			codeHooks = CodeHooks.Instance();
		}

		public CodeRegisterer(CodeHooks pCodeHooks)
        {
			codeHooks = pCodeHooks;
        }

		public void Register(SourceCodeInfo sourceCode)
		{
			dataReader = new DataReader(sourceCode.BaseDirPath);

			foreach(var sourceFile in sourceCode.SourceFiles)
			{
				RegisterFile(sourceFile);			
			}
		}

		private void RegisterFile(SourceFile sourceFile)
		{
			codeHooks.AddSourceFile(sourceFile.FPath, sourceFile.GetCode());
		}
	}
}