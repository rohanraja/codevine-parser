using System;
namespace CodePraser
{
	public class ProjectHookingPipeline
    {
		private readonly SourceCodeInfo sourceCodeInfo;
		private readonly string projectFileName;


		public ProjectHookingPipeline(string projectPath, string projectFileName)
		{
			ProjectParser projParser = new ProjectParser(projectPath, projectFileName);
			this.sourceCodeInfo = projParser.sourceCodeInfo;
			this.projectFileName = projectFileName;
		}

        public void Run()
		{
			ResetGitRepo();

			AddCodeHelperReference();

			RegisterFileContentsOnServer();

			HookAllSourceFiles();
		}

		private void AddCodeHelperReference()
		{
			ProjectReferenceInjector projectReferenceInjector = new ProjectReferenceInjector();
			projectReferenceInjector.InjectReference(sourceCodeInfo, projectFileName);

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
