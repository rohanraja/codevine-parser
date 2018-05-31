using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using CodeParserCommon;
using HooksInjector;
using CodePraser;

namespace HooksInjectorTests
{
    [TestClass]
    public class CodeElementsParseTests
    {
		private string dir;
		private SourceFile sourceFile;

        [TestMethod]
        public void TestGetterInjection()
        {
            var source = PipelineComponentsFactory.GetSourceFileForFile("ClassWithGetter.cs");
			var sourceFileAnalyzer = new SourceFileAnalyzer(source);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();

            Assert.IsTrue(blocks.Count == 1);
            Assert.IsTrue(blocks[0].IsMethod);
            Assert.IsTrue(blocks[0].methodName.Equals("testGetter.get"));
            Assert.IsTrue(blocks[0].Statements.Count == 2);
        }

        [TestMethod]
        public void TestSetterInjection()
        {
            var source = PipelineComponentsFactory.GetSourceFileForFile("WithSetter.cs");
			var sourceFileAnalyzer = new SourceFileAnalyzer(source);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();

            Assert.IsTrue(blocks.Count == 1);
            Assert.IsTrue(blocks[0].IsMethod);
            Assert.IsTrue(blocks[0].methodName.Equals("testSetter.set"));
            Assert.IsTrue(blocks[0].Statements.Count == 3);
        }


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


    }
}
