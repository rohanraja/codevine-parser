using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;

namespace CodeParserTests
{
    [TestClass]
    public class SolnProjectParserTest
    {

        [TestMethod]
        public void TestBuildAnalyzerParsingProject()
        {
			var sp = new BuildalyzerProjectParser();
			RunIProjectParserTest(sp);
        }

		private void RunIProjectParserTest(IProjectParser parser)
		{
			string repo = GitTests.RepoPath;
			var sci = parser.GetSourceCodeInfo(repo, GitTests.RepoCsProj);
            Assert.IsTrue(sci.CodeFiles.Count == 3);
            Assert.IsTrue(sci.CodeFiles.Contains("ClassA.cs"));
            Assert.IsTrue(sci.CodeFiles.Contains("ClassB.cs"));
            Assert.IsTrue(sci.CodeFiles.Contains("Program.cs"));

		}
    }
}
