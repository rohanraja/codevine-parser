using System;
using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class ClassInfo
    {
		public Dictionary<int, CodeRunnerInfo> CodeRunners = new Dictionary<int, CodeRunnerInfo>() { };
		public string RelativeFilePath;

		public ClassInfo()
        {
        }
		public void AddFieldInfo(FieldInfo fieldInfo, int id)
		{
			throw new NotImplementedException();
		}

		public void AddCodeRunnerInfo(CodeRunnerInfo codeRunnerInfo, int id)
		{
			CodeRunners[id] = codeRunnerInfo;
		}
	}
}
