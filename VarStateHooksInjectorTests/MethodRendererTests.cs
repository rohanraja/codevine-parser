using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjectorTests
{
    [TestClass]
    public class MethodRendererTests
    {
        [TestMethod]
        public void TestRoslynWorks()
        {
			string testMethod = @""" 
            void MethodA()
            {
                int count = 0;
                count ++;
            }
            """;

			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);

			Assert.IsTrue(methSyntax.Body.Statements.Count == 2);
        }

		[TestMethod]
        public void TestRenderingSimpleMethod_WithoutHookStatements()
		{
			string testMethod = @""" 
            void MethodA()
            {
                int count = 0;
                count ++;
            }
            """;
			int expectedStatementCount = 2;

			List<string> expectedStatementSubStrings = new List<string>(){
				"int count",
				"count ++"
			};

			RunBlockRenderTest(testMethod, expectedStatementCount, expectedStatementSubStrings);

		}

		private static void RunBlockRenderTest(string testMethod, int expectedStatementCount, List<string> expectedStatementSubStrings)
		{
			MethodDeclarationSyntax methSyntax = Helpers.ParseMethodSyntax(testMethod);
			CodeRunnerBlockWriter methSyntxWriter = CodeRunnerBlockWriter.GetWriter();
			CodeRunBlockRenderingInfo methodRenderingInfo = new CodeRunBlockRenderingInfo();
			BlockSyntax newBlock = methSyntxWriter.RenderMethodInfo(methodRenderingInfo, methSyntax.Body);

			Assert.IsTrue(newBlock.Statements.Count == expectedStatementCount);

			for (int i = 0; i < expectedStatementSubStrings.Count; i++)
			{
				Assert.IsTrue(newBlock.Statements[i].GetText().ToString().Contains(expectedStatementSubStrings[i]));
			}
		}
	}
}
