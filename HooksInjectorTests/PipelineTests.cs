using System;
using System.IO;
using CodeParserCommon;
using CodePraser;
using HooksInjector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HooksInjectorTests
{
	[TestClass]
    public class PipelineTests
	{
        private string dir;
        private SourceFile sourceFile;


        [TestInitialize]
        public void SetupSourceFile()
        {
            dir = GitTests.RepoPath;
            GitReset();
            var fName = "ClassA.cs";
            sourceFile = PipelineComponentsFactory.GetSourceFileForFile(fName);
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
        public void Test_INTEGRATION_SINGLE_FILE_PIPELINE_HOOK_TEST()
        {

			HookInjectionPipeline pipeline = new HookInjectionPipeline();
			string outText = pipeline.AddHooksToSourceFile(sourceFile);

            Assert.IsTrue(outText.Contains("OnMethodEnter"));
            Assert.IsTrue(outText.Contains("using CodeRecordHelpers;"));
            File.WriteAllText("/tmp/code.cs", outText);

        }
    }
}
