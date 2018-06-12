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
    public class StatementInfoCollectorTests
    {
		[TestMethod]
        public void TestSimpleIntField()
        {
            TestCase testCase = TestCase.GetLocalVariableDecClass();
			var stmt = Helpers.GetFirstNodeOfType<LocalDeclarationStatementSyntax>(testCase.Code);

			var coll = new StatementInfoCollector();
			var info = coll.Collect(stmt);

			Assert.IsTrue(info.IsLocalVarDeclaration);
			Assert.IsTrue(info.LocalVarNames.Contains("localVar1"));


        }

	}
}
