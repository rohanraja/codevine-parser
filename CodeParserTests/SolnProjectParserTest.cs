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
			var sp = new ProjectParser(repo, "TestCSharpProject.csproj");
			var sci = sp.sourceCodeInfo;
			Assert.IsTrue(sci.CodeFiles.Count == 3);
			Assert.IsTrue(sci.CodeFiles.Contains("ClassA.cs"));
			Assert.IsTrue(sci.CodeFiles.Contains("ClassB.cs"));
			Assert.IsTrue(sci.CodeFiles.Contains("Program.cs"));

        }
    }
}
