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

			Dictionary<int, string> renderingInfo = new Dictionary<int, string>() { };

			int expectedStatementCount = 2;

			List<string> expectedStatementSubStrings = new List<string>(){
				"int count",
				"count ++"
			};

			Helpers.RunBlockRenderTest(testMethod, renderingInfo, expectedStatementCount, expectedStatementSubStrings);
		}

	}
}
