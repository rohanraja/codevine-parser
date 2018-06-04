using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjectorTests
{
    public class Helpers
    {
        public Helpers()
        {
        }

		internal static MethodDeclarationSyntax ParseMethodSyntax(string testMethod)
		{
			var rt = SyntaxFactory.ParseSyntaxTree(testMethod).GetRoot();
            MethodDeclarationSyntax methSyntax = rt.ChildThatContainsPosition(0).AsNode() as MethodDeclarationSyntax;
			return methSyntax;
		}

		internal static void RunBlockRenderTest(string testMethod, int expectedStatementCount, List<string> expectedStatementSubStrings)
        {
        }

		internal static void RunBlockRenderTest(string testMethod, Dictionary<int, string> renderingInfo, int expectedStatementCount, List<string> expectedStatementSubStrings)
		{
			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);
            CodeRunnerBlockWriter methSyntxWriter = CodeRunnerBlockWriter.GetWriter();
			CodeRunBlockRenderingInfo methodRenderingInfo = createRenderingInfoFromDict(renderingInfo);
            BlockSyntax newBlock = methSyntxWriter.RenderMethodInfo(methodRenderingInfo, methSyntax.Body);

            Assert.IsTrue(newBlock.Statements.Count == expectedStatementCount);

            for (int i = 0; i < expectedStatementSubStrings.Count; i++)
            {
                Assert.IsTrue(newBlock.Statements[i].GetText().ToString().Contains(expectedStatementSubStrings[i]));
            }
		}

		internal static CodeRunBlockRenderingInfo createRenderingInfoFromDict(Dictionary<int, string> renderingInfo)
		{
			return new CodeRunBlockRenderingInfo();
		}
	}
}
