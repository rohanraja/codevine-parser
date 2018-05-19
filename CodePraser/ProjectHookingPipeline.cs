using System;
namespace CodePraser
{
	public class ProjectHookingPipeline
    {
		private readonly SourceCodeInfo sourceCodeInfo;

		public ProjectHookingPipeline(SourceCodeInfo sourceCodeInfo)
        {
			this.sourceCodeInfo = sourceCodeInfo;
		}

		public ProjectHookingPipeline(string projectPath, string projectFileName)
		{
			ProjectParser projParser = new ProjectParser(projectPath, projectFileName);
			this.sourceCodeInfo = projParser.sourceCodeInfo;
		}

        public void Run()
		{
			ResetGitRepo();

			RegisterFileContentsOnServer();

			HookAllSourceFiles();
		}

		private void HookAllSourceFiles()
		{
			SourceFileHooker sourceFileHooker = new SourceFileHooker();

			foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
				sourceFileHooker.AddHooksToSourceFile(sourceFile);
            }
		}

		private void ResetGitRepo()
		{
			var git = new GitHelpers();
			git.ResetHard(sourceCodeInfo.BaseDirPath);
		}

		private void RegisterFileContentsOnServer()
		{
			CodeRegisterer codeRegisterer = new CodeRegisterer();
			codeRegisterer.Register(sourceCodeInfo);
		}

	}
}
