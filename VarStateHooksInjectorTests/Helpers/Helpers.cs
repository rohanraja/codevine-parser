using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace VarStateHooksInjectorTests
{

	public class Helpers
    {
        public Helpers()
        {
        }

		internal static SyntaxTree GetRoot(string code)
        {
			var root = SyntaxFactory.ParseSyntaxTree(code);
			return root;
        }

        internal static T GetFirstNodeOfType<T>(string code)
		{
			var root = SyntaxFactory.ParseSyntaxTree(code).GetRoot();
			return GetFirstNodeOfType<T>(root);
            
		}
		internal static MethodDeclarationSyntax GetFirstMethodSyntax(string testMethod)
		{
			return GetFirstNodeOfType<MethodDeclarationSyntax>(testMethod);
		}

		internal static T GetFirstNodeOfType<T>(SyntaxNode root)
		{
			var meth = from methodDeclaration in root.DescendantNodes()
                                                    .OfType<T>()
                       select methodDeclaration;
            return meth.First();
		}

		internal static ClassDeclarationSyntax GetFirstClassSyntax(string testMethod)
        {
			return GetFirstNodeOfType<ClassDeclarationSyntax>(testMethod);
        }


		internal static void RunBlockRenderTest(string testMethod, int expectedStatementCount, List<string> expectedStatementSubStrings)
        {
        }

		internal static void RunBlockRenderTest(string testMethod, Dictionary<int, List<string>> renderingInfo, int expectedStatementCount, List<string> expectedStatementSubStrings)
		{
			MethodDeclarationSyntax methSyntax = Helpers.GetFirstMethodSyntax(testMethod);
			CodeRunnerBlockRenderer methSyntxWriter = CodeRunnerBlockRenderer.GetWriter();
			CodeRunBlockRenderingInfo methodRenderingInfo = createRenderingInfoFromDict(renderingInfo);
			BlockSyntax newBlock = methSyntxWriter.RenderMethodInfo(methodRenderingInfo, methSyntax.Body);

			CheckExpectedStatements(expectedStatementCount, expectedStatementSubStrings, newBlock);
		}

		public static void CheckExpectedStatements(int expectedStatementCount, List<string> expectedStatementSubStrings, BlockSyntax newBlock)
		{
			var statements = getAllStatements(newBlock);

			//Assert.IsTrue(statements.Count == expectedStatementCount);

			for (int i = 0; i < expectedStatementSubStrings.Count; i++)
			{
				//Assert.IsTrue(statements[i].GetText().ToString().Contains(expectedStatementSubStrings[i]));
				Assert.IsTrue(StatementSyntaxContains(statements, expectedStatementSubStrings[i]));
			}
		}

		private static bool StatementSyntaxContains(List<StatementSyntax> statements, string v)
		{
			for (int i = 0; i < statements.Count; i++)
			{
				if (statements[i].GetText().ToString().Contains(v))
					return true;

			}
			return false;
		}

		public static List<StatementSyntax> getAllStatements(BlockSyntax block)
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
