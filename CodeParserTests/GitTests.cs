using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;

namespace CodeParserTests
{
	[TestClass]
    public class GitTests
    {
		public static string RepoPath = "/Users/rohan/code/codevine_parser/CodeVine_Parser/TestCSharpProject";

		public static string RepoCsProj = "TestCSharpProject.csproj";

        //[TestMethod]
        public void TestGitReset()
        {

			var git = new GitHelpers();
			git.ResetHard(RepoPath);
        }
    }
}
