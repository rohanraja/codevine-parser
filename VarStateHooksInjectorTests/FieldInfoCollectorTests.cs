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
    public class FieldInfoCollectorTests
    {
		[TestMethod]
        public void TestSimpleIntField()
        {
            TestCase testCase = TestCase.GetClassWithIntField();
			var finfo = RunFieldCollectorTest(testCase);
			Assert.IsFalse(finfo.IsStatic);
        }

		[TestMethod]
        public void TestStaticFieldIsIgnored()
        {
            TestCase testCase = TestCase.GetClassWithStaticIntField();
            var finfo = RunFieldCollectorTest(testCase);
			Assert.IsTrue(finfo.IsStatic);
        }

		private FieldInfo RunFieldCollectorTest(TestCase testCase)
		{
			var fieldNode = Helpers.GetFirstNodeOfType<FieldDeclarationSyntax>(testCase.Code);
			FieldInfoCollector fieldInfoCollector = new FieldInfoCollector();
			var finfo = fieldInfoCollector.Collect(fieldNode);
			Assert.IsTrue(finfo.Names[0] == "field1");
			Assert.IsNotNull(finfo.Type);
			Assert.IsNotNull(finfo.Modifiers);
			return finfo;
		}
	}
}
