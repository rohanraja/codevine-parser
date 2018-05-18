using System;
using CodeReader;
using CodeRecordHelpers;

namespace CodePraser
{
	public class CodeRegisterer
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

			foreach(string fname in sourceCode.CodeFiles)
			{
				RegisterFile(fname);			
			}
		}

		private void RegisterFile(string fname)
		{
			string code = dataReader.GetContentsOfFileAtRoot(fname);
			codeHooks.AddSourceFile(fname, code);
		}
	}
}