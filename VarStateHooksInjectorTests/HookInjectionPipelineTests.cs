using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using System.Collections.Generic;
namespace VarStateHooksInjectorTests
{
	[TestClass]
    public class HookInjectionPipelineTests
    {
		[TestMethod]
        public void TestForSimple2LineMethodFile()
        {
            TestCase testCase = TestCase.GetSimple2LineMethodCase();
            RunEndToEndHookInjection(testCase);
        }

		[TestMethod]
        public void TestForSingleIfMethodFile()
        {
			TestCase testCase = TestCase.GetSingleIfMethod();
            RunEndToEndHookInjection(testCase);
        }

		[TestMethod]
        public void TestForMethodArgumentLocalVarHooks()
        {
            TestCase testCase = TestCase.GetBlankMethodWithSingleArgument();
            RunEndToEndHookInjection(testCase);
        }

		private void RunEndToEndHookInjection(TestCase testCase)
		{
			var fname = testCase.FileName;
			var contents = testCase.Code;

			HookInjectionPipeline pipeline = new HookInjectionPipeline();
			string newContents = pipeline.AddHooksToSourceFile(fname, contents);

			var methoNode = Helpers.GetFirstNodeOfType<MethodDeclarationSyntax>(newContents);
            Helpers.CheckExpectedStatements(testCase.ExpectedStatementCount, testCase.ExpectedStatements, methoNode.Body);
		}
	}
}
