using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;

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

		internal static void RunBlockRenderTest(string testMethod, Dictionary<int, List<string>> renderingInfo, int expectedStatementCount, List<string> expectedStatementSubStrings)
		{
			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);
            CodeRunnerBlockRenderer methSyntxWriter = CodeRunnerBlockRenderer.GetWriter();
			CodeRunBlockRenderingInfo methodRenderingInfo = createRenderingInfoFromDict(renderingInfo);
            BlockSyntax newBlock = methSyntxWriter.RenderMethodInfo(methodRenderingInfo, methSyntax.Body);

			var statements = getAllStatements(newBlock);

            Assert.IsTrue(statements.Count == expectedStatementCount);

            for (int i = 0; i < expectedStatementSubStrings.Count; i++)
            {
				Assert.IsTrue(statements[i].GetText().ToString().Contains(expectedStatementSubStrings[i]));
            }
		}

		private static List<StatementSyntax> getAllStatements(BlockSyntax block)
		{
			var stateColl = new StatementsCollector();
			stateColl.Visit(block);
			return stateColl.Statements;
		}

		internal static CodeRunBlockRenderingInfo createRenderingInfoFromDict(Dictionary<int, List<string>> renderingInfo)
		{
			return new CodeRunBlockRenderingInfo(renderingInfo);
		}
	}
}
