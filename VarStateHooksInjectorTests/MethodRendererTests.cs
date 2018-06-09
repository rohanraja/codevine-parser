using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjectorTests
{
    // Todo - Use TestCase class to generate test data
    [TestClass]
    public class MethodRendererTests
    {
		[TestMethod]
        public void TestMethodWithMultiLevelIfBlock()
        {
            string testMethod = @""" 
            void MethodA()
            {
                if(true)
                {
                    if(true)
                    {
                        int count = 0;
                    }
                    int midCount = 0;
                }
            }
            """;

            Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };
			renderingInfo[0] = new List<string>(){
				"IfHookCall()",
				"0",
			};
			renderingInfo[1] = new List<string>(){
				"IfHookCall()",
                "0",
				"AssignmentCall()",
				"1",
            };
			renderingInfo[2] = new List<string>(){
                "AssignmentCall()",
                "0",
            };

            int expectedStatementCount = 8;

            List<string> expectedStatementSubStrings = new List<string>(){
				"IfHookCall()",
				"if(true)",
				"IfHookCall()",
                "if(true)",
				"AssignmentCall()",
                "int midCount",
				"AssignmentCall()",
				"int count",
            };

            Helpers.RunBlockRenderTest(testMethod, renderingInfo, expectedStatementCount, expectedStatementSubStrings);
        }

		[TestMethod]
		public void TestMethodWithSingleIfBlock()
		{
			string testMethod = @""" 
            void MethodA()
            {
                if(true)
                {
                    int count = 0;
                }
            }
            """;

			Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };
			renderingInfo[0] = new List<string>(){
				"IfHookCall()",
				"0",
			};
			renderingInfo[1] = new List<string>(){
				"AssignmentCall()",
				"0",
			};

			int expectedStatementCount = 4;

			List<string> expectedStatementSubStrings = new List<string>(){
				"IfHookCall()",
				"if(true)",
				"AssignmentCall()",
				"int count",
			};

			Helpers.RunBlockRenderTest(testMethod, renderingInfo, expectedStatementCount, expectedStatementSubStrings);
		}


		[TestMethod]
        public void TestRenderingSimpleMethod_WithHookStatements()
        {
			TestCase testCase = TestCase.GetSimple2LineMethodCase();
			Helpers.RunBlockRenderTest(testCase.Code, testCase.RenderInfo, testCase.ExpectedStatementCount, testCase.ExpectedStatements);
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

			MethodDeclarationSyntax methSyntax = Helpers.GetFirstMethodSyntax(testMethod);

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
