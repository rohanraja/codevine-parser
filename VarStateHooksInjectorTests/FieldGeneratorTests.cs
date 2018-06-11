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
    public class FieldGeneratorTests
    {
		[TestMethod]
        public void TestSimpleIntField()
        {
            TestCase testCase = TestCase.GetClassWithIntField();
			RunFieldGeneratorTest(testCase);
        }

		private void RunFieldGeneratorTest(TestCase testCase)
		{
			var fieldNode = Helpers.GetFirstNodeOfType<FieldDeclarationSyntax>(testCase.Code);
			FieldGenerator generator = new FieldGenerator(null);
			var newFieldNode = generator.Generate(fieldNode, 0);

			foreach(var VarDec in newFieldNode.Declaration.Variables)
			{
				Assert.IsTrue(VarDec.Identifier.ToString().Contains(FieldGenerator.VAR_PREFIX));
			}


		}
	}
}
