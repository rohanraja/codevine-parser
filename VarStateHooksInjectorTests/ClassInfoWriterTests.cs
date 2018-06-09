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
    public class ClassInfoWriterTests
    {
		[TestMethod]
        public void TestSimpleMethodWriting_INTEGRATION()
		{
			TestCase testCase = TestCase.GetSimple2LineMethodCase();
			RunClassInfoWriterTest(testCase);
		}

		[TestMethod]
        public void TestNestedMethodWriting_INTEGRATION()
        {
			TestCase testCase = TestCase.GetSingleIfMethod();
            RunClassInfoWriterTest(testCase);
        }

        private static void RunClassInfoWriterTest(TestCase testCase)
        {
            ClassInfoWriter writer = new ClassInfoWriter(testCase.ClassInfo);
            var newNode = writer.Visit(testCase.Node);

            var methoNode = newNode as MethodDeclarationSyntax;
            Helpers.CheckExpectedStatements(testCase.ExpectedStatementCount, testCase.ExpectedStatements, methoNode.Body);
        }
    }
}
