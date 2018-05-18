using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;

namespace CodeParserTests
{
    [TestClass]
    public class GitTests
    {

        //[TestMethod]
        public void TestGitReset()
        {
			string repoPath = "/Users/rohan/code/codevine_parser/CodeVine_Parser/TestCSharpProject";

			var git = new GitHelpers();
			git.ResetHard(repoPath);
        }
    }
}
