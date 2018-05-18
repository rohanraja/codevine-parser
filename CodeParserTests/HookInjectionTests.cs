using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodePraser.HooksInjection;

namespace CodeParserTests
{
    [TestClass]
    public class HookInjectionTests
    {
        [TestMethod]
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


        }
    }
}
