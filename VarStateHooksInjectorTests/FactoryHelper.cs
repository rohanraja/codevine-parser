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

			return classInfo;
        }

		public static CodeRunnerInfo GenerateCodeRunnerInfo(int numStatements=1, int level2Staements=0)
		{
            
			CodeRunnerInfo info = new CodeRunnerInfo();
			info.Name = "TestMethod";
			info.IsConstructor = false;

			info.blockInfo[0] = new List<StatementInfo>() { };

			for (int i = 0; i < numStatements; i++)
			{
                StatementInfo sInfo = new StatementInfo();
                sInfo.LineNo = 9+i;
                info.blockInfo[0].Add(sInfo);
			}

			if(level2Staements > 0)
			{
				info.blockInfo[1] = new List<StatementInfo>() { };

				for (int i = 0; i < level2Staements; i++)
                {
                    StatementInfo sInfo = new StatementInfo();
                    sInfo.LineNo = 9 + i;
                    info.blockInfo[1].Add(sInfo);
                }
			}

			return info;
		}
	}
}
