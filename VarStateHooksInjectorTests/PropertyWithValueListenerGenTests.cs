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
			fieldInfo.Names.Add("field1");

			fieldInfo.Type = fieldNode.Declaration.Type;
			fieldInfo.Modifiers = fieldNode.Modifiers;

            var PropNode = gen.GenerateProperty(fieldInfo);

			var getter = Helpers.GetFirstNodeOfType<AccessorDeclarationSyntax>(PropNode[0]);
			Assert.IsTrue(getter.Body.Statements[0].GetText().ToString().Contains(fieldInfo.Names[0]));
        }

		[TestMethod]
		public void TestCodeVinePropertyGeneration()
		{
			var gen = new PropertyWithValueListenerGen();
			var fieldSyntax = gen.GetCodeVineClrIdFieldSyntax();

			string txt = fieldSyntax.GetText().ToString();
			Assert.IsTrue(txt.Contains("GetHashCode"));
			Assert.IsTrue(txt.Contains("CodeVine"));
			Assert.IsTrue(txt.Contains("int"));
			Assert.IsTrue(txt.Contains("public"));

		}

	}
}
