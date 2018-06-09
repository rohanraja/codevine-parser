using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using System.Collections.Generic;
using System.IO;

namespace VarStateHooksInjectorTests
{
	[TestClass]
    public class CSFileInfoWriterTests
    {
		[TestMethod]
        public void TestSimpleMethodWriting_INTEGRATION()
		{
			TestCase testCase = TestCase.GetSimple2LineMethodCase();
			RunCSFileInfoWriterTest(testCase);
		}

		[TestMethod]
        public void TestNestedMethodWriting_INTEGRATION()
        {
			TestCase testCase = TestCase.GetSingleIfMethod();
            RunCSFileInfoWriterTest(testCase);
        }

        private static void RunCSFileInfoWriterTest(TestCase testCase)
        {
			CSFileInfoWriter writer = new CSFileInfoWriter(testCase.CSFileInfo);
			var newRoot = writer.Visit(testCase.Root.GetRoot());

			//var text = newRoot.GetText().ToString();
			//File.WriteAllText("/tmp/test.cs", text);

			var methoNode = Helpers.GetFirstNodeOfType<MethodDeclarationSyntax>(newRoot);
            Helpers.CheckExpectedStatements(testCase.ExpectedStatementCount, testCase.ExpectedStatements, methoNode.Body);
        }
    }
}
