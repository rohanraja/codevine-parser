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
        public void TestRenderingSimpleMethod_WithHookStatements()
        {
            string testMethod = @""" 
            void MethodA()
            {
                int count = 0;
                count ++;
            }
            """;

            Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };
			renderingInfo[0] = new List<string>(){
				"0",
				"int hook1 = 5;",
				"1",
				"int hook2 = 10"
			};

            int expectedStatementCount = 4;

            List<string> expectedStatementSubStrings = new List<string>(){
                "int count",
				"int hook1 = 5;",
                "count ++",
                "int hook2 = 10"
            };

            Helpers.RunBlockRenderTest(testMethod, renderingInfo, expectedStatementCount, expectedStatementSubStrings);
        }

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

			var lines = testMethod.Split('\n');
			var trimmed = lines[1].Trim();
			Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };

			int expectedStatementCount = 2;

			List<string> expectedStatementSubStrings = new List<string>(){
				"int count",
				"count ++"
			};

			Helpers.RunBlockRenderTest(testMethod, renderingInfo, expectedStatementCount, expectedStatementSubStrings);
		}

	}
}
