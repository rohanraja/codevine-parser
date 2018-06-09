using System;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace VarStateHooksInjectorTests
{
    public class TestCase
    {
		public string Code;
		public ClassInfo ClassInfo;
		public List<string> ExpectedStatements { get; private set; }
        public int ExpectedStatementCount { get; private set; }
		public SyntaxNode Node { get; private set; }
		public Dictionary<int, List<string>> RenderInfo { get; private set; }
		public SyntaxTree Root { get; internal set; }

		public TestCase()
        {
        }


		public static TestCase GetSimple2LineMethodCase()
		{
			string testMethod = @""" 
            class TestClass
            {
                void MethodA()
                {
                    int count = 0;
                    count ++;
                }
            }
            """;
			ClassInfo classInfo = FactoryHelper.CreateClassInfo();

			CodeRunnerInfo codeRunnerInfo = new CodeRunnerInfo();
            codeRunnerInfo.Name = "TestMethod";
            codeRunnerInfo.IsConstructor = false;
            
            codeRunnerInfo.blockInfo[0] = new List<StatementInfo>() { };
            StatementInfo sInfo = new StatementInfo();
            sInfo.LineNo = 5;
            codeRunnerInfo.blockInfo[0].Add(sInfo);
			var sInfo2 = new StatementInfo();
            sInfo2.LineNo = 6;
            codeRunnerInfo.blockInfo[0].Add(sInfo2);

            classInfo.AddCodeRunnerInfo(codeRunnerInfo, 0);

			Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };
            renderingInfo[0] = new List<string>(){
				"OnMethodEnter();",
				"LogLineRun()",
				"0",
				"LogLineRun()",
				"1",
            };

			List<string> expectedStatementSubStrings = new List<string>(){
                "OnMethodEnter",
                "LogLineRun",
                "int count",
                "LogLineRun",
                "count ++",
            };

			SyntaxNode methSyntax = Helpers.GetFirstMethodSyntax(testMethod);

			SyntaxTree root = Helpers.GetRoot(testMethod);

			return new TestCase
			{
				Code = testMethod,
				ClassInfo = classInfo,
				ExpectedStatements = expectedStatementSubStrings,
                ExpectedStatementCount = 5,
				Node = methSyntax,
				RenderInfo = renderingInfo,
                Root = root
			};
		}

		public static TestCase GetSingleIfMethod()
        {
			string testMethod = @""" 
            class TestClass
            {
                void MethodA()
                {
                    int b=3;
                    if(true)
                    {
                        int count = 0;
                    }
                }
            }
            """;
            MethodDeclarationSyntax methSyntax = Helpers.GetFirstMethodSyntax(testMethod);
            ClassInfo classInfo = FactoryHelper.CreateClassInfo();

			CodeRunnerInfo codeRunnerInfo = new CodeRunnerInfo();
            codeRunnerInfo.Name = "TestMethod";
            codeRunnerInfo.IsConstructor = false;
            
            codeRunnerInfo.blockInfo[0] = new List<StatementInfo>() { };
			codeRunnerInfo.blockInfo[1] = new List<StatementInfo>() { };
            StatementInfo sInfo = new StatementInfo();
            sInfo.LineNo = 5;
            codeRunnerInfo.blockInfo[0].Add(sInfo);
			sInfo = new StatementInfo();
            sInfo.LineNo = 6;
            codeRunnerInfo.blockInfo[0].Add(sInfo);
            var sInfo2 = new StatementInfo();
            sInfo2.LineNo = 8;
            codeRunnerInfo.blockInfo[1].Add(sInfo2);

            classInfo.AddCodeRunnerInfo(codeRunnerInfo, 0);
			SyntaxTree root = Helpers.GetRoot(testMethod);

            List<string> expectedStatementSubStrings = new List<string>(){
                "OnMethodEnter",
                "LogLineRun",
                "int b",
                "LogLineRun",
                "if(true)",
                "LogLineRun",
                "int count",
            };

            return new TestCase
            {
                Code = testMethod,
                ClassInfo = classInfo,
                ExpectedStatements = expectedStatementSubStrings,
                ExpectedStatementCount = 7,
                Node = methSyntax,
                Root = root
            };
        }
    }
}
