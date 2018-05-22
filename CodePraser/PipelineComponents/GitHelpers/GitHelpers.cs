using System;
using LibGit2Sharp;
using CodeParserCommon;

namespace CodePraser
{
    public class GitHelpers : IGitHelpers
	{
        public GitHelpers()
        {
        }

		public void ResetHard(string repoPath)
		{
			using (var repo = new Repository(repoPath))
            {
                Branch originMaster = repo.Branches["master"];
                repo.Reset(ResetMode.Hard, originMaster.Tip);
            }
		}
	}
}
