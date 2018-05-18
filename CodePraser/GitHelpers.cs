using System;
using LibGit2Sharp;

namespace CodePraser
{
    public class GitHelpers
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
