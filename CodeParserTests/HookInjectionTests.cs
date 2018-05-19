﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodePraser.HooksInjection;
using System.IO;

namespace CodeParserTests
{
    [TestClass]
    public class HookInjectionTests
    {
        //[TestMethod]
        public void TestParsingCodeFiles()
        {

			var dir = GitTests.RepoPath;
			var fName = "ClassA.cs";

			var sourceFile = new SourceFile(fName, dir);

			var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);

			var blocks = sourceFileAnalyzer.GetCodeBlocks();

			Assert.IsTrue(blocks.Count > 0);
			Assert.IsTrue(blocks[2].methodName == "GetCount");
			Assert.IsTrue(blocks[0].methodName == "ClassA");

			Assert.IsTrue(blocks[1].Statements[0].LineNo == 19);

			HooksRenderer hooksRenderer = new HooksRenderer(sourceFile, blocks);

			string outText = hooksRenderer.GetHookedCode();
			File.WriteAllText("/tmp/code.cs", outText);

			Assert.IsTrue(outText.Contains("OnMethodEnter"));

        }

		[TestMethod]
        public void FullTest()
		{
			ProjectParser projParser = new ProjectParser(GitTests.RepoPath, GitTests.RepoCsProj);
			var sourceCode = projParser.sourceCodeInfo;

			HookingPipeline parser = new HookingPipeline(sourceCode);
			parser.Run();
		}
    }
}
