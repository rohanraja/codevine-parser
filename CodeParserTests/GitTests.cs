using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using System;
using CodeParserCommon;

namespace CodeParserTests
{
	[TestClass]
    public class GitTests
    {
		public static string RepoPath = @"C:\src\codevine-parser\TestCSharpProject";

		public static string RepoCsProj = "TestCSharpProject.csproj";

        //[TestMethod]
        public void TestGitReset()
        {

			var git = new GitHelpers();
			git.ResetHard(RepoPath);
        }

		internal static SourceCodeInfo GetSourceCode()
		{
			throw new NotImplementedException();
		}
	}
}
