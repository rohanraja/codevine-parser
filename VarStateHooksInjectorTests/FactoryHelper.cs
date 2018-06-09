using System;
using System.Collections.Generic;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjectorTests
{
    public class FactoryHelper
    {

		public static ClassInfo CreateClassInfo()
        {
			ClassInfo classInfo = new ClassInfo();
			classInfo.RelativeFilePath = "TestFile.cs";

			CodeRunnerInfo codeRunnerInfo = GenerateCodeRunnerInfo();

			classInfo.AddCodeRunnerInfo(codeRunnerInfo, 0);

			return classInfo;
        }

		public static CodeRunnerInfo GenerateCodeRunnerInfo()
		{
            
			CodeRunnerInfo info = new CodeRunnerInfo();
			info.Name = "TestMethod";
			info.IsConstructor = false;

			StatementInfo sInfo = new StatementInfo();
			sInfo.LineNo = 9;

			info.blockInfo[0] = new List<StatementInfo>() { };
			info.blockInfo[0].Add(sInfo);

			return info;
		}
	}
}
