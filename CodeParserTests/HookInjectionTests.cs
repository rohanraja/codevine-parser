using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodePraser.HooksInjection;
using System.IO;
using System.Collections.Generic;
using CodePraser.PipelineComponents.HooksInjection;

namespace CodeParserTests
{
    [TestClass]
    public class HookInjectionTests
    {
		private string dir;
		private SourceFile sourceFile;

		[TestInitialize]
		public void SetupSourceFile()
		{
			dir = GitTests.RepoPath;
			GitReset();
            var fName = "ClassA.cs";
            sourceFile = new SourceFile(fName, dir);
		}

		[TestCleanup]
        public void CleanUpGit()
		{
			GitReset();
		}

		private void GitReset()
		{
			var git = new GitHelpers();
			git.ResetHard(dir);
		}

		[TestMethod]
        public void Test_INTEGRATION_ANALYSER_HOOKGENERATOR_RENDERER()
        {

			var sourceFileAnalyzer = new SourceFileAnalyzer(this.sourceFile);
			var blocks = sourceFileAnalyzer.GetCodeBlocks();

			CodeblocksToHooksGenerator gen = new CodeblocksToHooksGenerator();
            var hooksList = gen.GenerateHooks(blocks);

			HooksRenderer hooksRenderer = new HooksRenderer();
			string outText = hooksRenderer.GetHookedCode(sourceFile, hooksList);

			Assert.IsTrue(outText.Contains("OnMethodEnter"));
			Assert.IsTrue(outText.Contains("using CodeRecordHelpers;"));
			File.WriteAllText("/tmp/code.cs", outText);

        }

		[TestMethod]
		public void TestCodeAnalyzer_UNIT()
		{
			var sourceFileAnalyzer = new SourceFileAnalyzer(this.sourceFile);
            var blocks = sourceFileAnalyzer.GetCodeBlocks();

            Assert.IsTrue(blocks.Count > 0);
            Assert.IsTrue(blocks[2].methodName == "GetCount");
            Assert.IsTrue(blocks[0].methodName == "ClassA");

		}

		[TestMethod]
        public void TestHooksRenderer_UNIT()
		{
			List<Hooks> hooksList = PipelineComponentsFactory.GenerateHookList(sourceFile);

            HooksRenderer hooksRenderer = new HooksRenderer();
            string outText = hooksRenderer.GetHookedCode(sourceFile, hooksList);

			Assert.IsTrue(outText.Contains("OnMethodEnter"));
			Assert.IsTrue(outText.Contains("LogLineRun"));
			Assert.IsTrue(outText.Contains("using CodeRecordHelpers;"));
			File.WriteAllText("/tmp/code.cs", outText);

		}

		[TestMethod]
		public void TestCodeBlockToHooksGenerator_UNIT()
		{
			List<CodeBlock> blocks = PipelineComponentsFactory.GenerateTestCodeBlock(sourceFile);

			CodeblocksToHooksGenerator gen = new CodeblocksToHooksGenerator();

			var hooksList = gen.GenerateHooks(blocks);

			Assert.IsTrue(hooksList.Count > 0);
			Assert.IsTrue(hooksList[1].Pairs[1].Key == 0);
			Assert.IsTrue(hooksList[1].Pairs[0].Value.Contains("var mrid = CodeHooks.Instance().OnMethodEnter(\"ClassA.cs\", \"MethodA_1\");\n\n"));
			Assert.IsTrue(hooksList[1].Pairs[1].Value.Contains("CodeHooks.Instance().LogLineRun(mrid, 19"));
            
		}

		//[TestMethod]
        public void FullTest()
		{
			//ProjectHookingPipeline parser = new ProjectHookingPipeline(GitTests.RepoPath, GitTests.RepoCsProj);
			//parser.Run();
		}

        //[TestMethod]
        public void TestSourceFileHookerImpl()
        {
			SourceFileHooker sourceFileHooker = new SourceFileHooker();
			TestISourceCodeHooker(sourceFileHooker);
        }

		void TestISourceCodeHooker(ISourceFileHooker sourceFileHooker)
		{
			SourceCodeInfo sourceCode = GitTests.GetSourceCode();
			sourceFileHooker.AddHooksToSourceCode(sourceCode);
			
		}
    }
}
