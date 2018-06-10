using System;
using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class ClassInfo
    {
		public Dictionary<int, CodeRunnerInfo> CodeRunners = new Dictionary<int, CodeRunnerInfo>() { };
		public Dictionary<int, FieldInfo> FieldInfos = new Dictionary<int, FieldInfo>() { };

		public string RelativeFilePath;
        public string Name;

     
		public ClassInfo()
        {
        }
		public void AddFieldInfo(FieldInfo fieldInfo, int id)
		{
			FieldInfos[id] = fieldInfo;
		}

		public FieldInfo GetFieldInfo(int id)
        {
			return FieldInfos[id];
        }

		public CodeRunnerInfo GetCodeRunnerInfo(int id)
		{
			return CodeRunners[id];
		}

		public void AddCodeRunnerInfo(CodeRunnerInfo codeRunnerInfo, int id)
		{
			CodeRunners[id] = codeRunnerInfo;
		}
	}
}
