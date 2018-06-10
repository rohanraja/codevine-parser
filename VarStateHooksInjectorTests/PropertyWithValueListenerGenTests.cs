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
    public class PropertyWithValueListenerGenTests
    {
		[TestMethod]
        public void TestSimpleIntField()
        {
            TestCase testCase = TestCase.GetClassWithIntField();
			var fieldNode = Helpers.GetFirstNodeOfType<FieldDeclarationSyntax>(testCase.Code);
			var propNode = Helpers.GetFirstNodeOfType<PropertyDeclarationSyntax>(testCase.Code);
            var gen = new PropertyWithValueListenerGen();

            FieldInfo fieldInfo = new FieldInfo();
			fieldInfo.Name = "field1";

			fieldInfo.Type = fieldNode.Declaration.Type;
			fieldInfo.Modifiers = fieldNode.Modifiers;

            var PropNode = gen.GenerateProperty(fieldInfo);

			var getter = Helpers.GetFirstNodeOfType<AccessorDeclarationSyntax>(PropNode);
			Assert.IsTrue(getter.Body.Statements[0].GetText().ToString().Contains(fieldInfo.Name));
        }

	}
}
