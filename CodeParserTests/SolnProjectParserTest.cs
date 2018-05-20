using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;

namespace CodeParserTests
{
    [TestClass]
    public class SolnProjectParserTest
    {

        [TestMethod]
        public void TestParsingProject()
        {
			string repo = GitTests.RepoPath;
			var sp = new ProjectParser();
			var sci = sp.GetSourceCodeInfo(repo, GitTests.RepoCsProj);
			Assert.IsTrue(sci.CodeFiles.Count == 3);
			Assert.IsTrue(sci.CodeFiles.Contains("ClassA.cs"));
			Assert.IsTrue(sci.CodeFiles.Contains("ClassB.cs"));
			Assert.IsTrue(sci.CodeFiles.Contains("Program.cs"));

        }
    }
}
