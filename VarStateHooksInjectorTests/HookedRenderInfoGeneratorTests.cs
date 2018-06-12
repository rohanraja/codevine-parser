using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjectorTests
{
	[TestClass]
    public class HookedRenderInfoGeneratorTests
    {
		[TestMethod]
        public void TestMethodBlockGenerator()
		{
			HookedRenderInfoGenerator generator = new HookedRenderInfoGenerator();
			ClassInfo classInfo = FactoryHelper.CreateClassInfo();
			CodeRunnerInfo codeRunnerInfo = FactoryHelper.GenerateCodeRunnerInfo();
            classInfo.AddCodeRunnerInfo(codeRunnerInfo, 0);
            int id = 0;

            CodeRunBlockRenderingInfo renderInfo = generator.CodeRunBlockRenderInfoForMethod(classInfo, id);

			Assert.IsTrue(renderInfo.renderingInfo[0][0].Contains("OnMethodEnter"));
			Assert.IsTrue(renderInfo.renderingInfo[0][1].Contains("LogLineRun"));
			Assert.IsTrue(renderInfo.renderingInfo[0][2].Contains("0"));
		}
        
		[TestMethod]
        public void TestLocalVarDeclarationHooks()
        {
            HookedRenderInfoGenerator generator = new HookedRenderInfoGenerator();
			ClassInfo classInfo = TestCase.GetLocalVarAssignmentCode().ClassInfo;

            int id = 0;

            CodeRunBlockRenderingInfo renderInfo = generator.CodeRunBlockRenderInfoForMethod(classInfo, id);
            
            Assert.IsTrue(renderInfo.renderingInfo[0][0].Contains("OnMethodEnter"));
            Assert.IsTrue(renderInfo.renderingInfo[0][1].Contains("LogLineRun"));
            Assert.IsTrue(renderInfo.renderingInfo[0][2].Contains("0"));
			Assert.IsTrue(renderInfo.renderingInfo[0][3].Contains("LocalVarUpdate"));
			Assert.IsTrue(renderInfo.renderingInfo[0][3].Contains("localVar1"));
			Assert.IsTrue(renderInfo.renderingInfo[0][4].Contains("LogLineRun"));
			Assert.IsTrue(renderInfo.renderingInfo[0][5].Contains("1"));
			Assert.IsTrue(renderInfo.renderingInfo[0][6].Contains("LocalVarUpdate"));
			Assert.IsTrue(renderInfo.renderingInfo[0][6].Contains("localVar1"));
        }
    }
}
