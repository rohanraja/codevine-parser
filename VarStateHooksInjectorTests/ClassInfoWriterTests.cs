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
        public void TestMethodWriting_INTEGRATION()
        {
			string testMethod = @""" 
            void MethodA()
            {
                int count = 0;
                count ++;
            }
            """;
			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);
			ClassInfo classInfo = FactoryHelper.CreateClassInfo();
            CodeRunnerInfo codeRunnerInfo = FactoryHelper.GenerateCodeRunnerInfo(2);
            classInfo.AddCodeRunnerInfo(codeRunnerInfo, 0);
			List<string> expectedStatementSubStrings = new List<string>(){
                "OnMethodEnter",
				"LogLineRun",
				"int count",
                "LogLineRun",
                "count ++",
            };

			ClassInfoWriter writer = new ClassInfoWriter(classInfo);
			var newNode = writer.Visit(methSyntax);

			var methoNode = newNode as MethodDeclarationSyntax;
			Helpers.CheckExpectedStatements(5, expectedStatementSubStrings, methoNode.Body);

        }
    }
}
