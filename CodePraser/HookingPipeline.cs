using CodePraser.HooksInjection;
using System;
namespace CodePraser
{
    public class HookingPipeline
    {
		private readonly SourceCodeInfo sourceCodeInfo;

		public HookingPipeline(SourceCodeInfo sourceCodeInfo)
        {
			this.sourceCodeInfo = sourceCodeInfo;
		}

        public void Run()
		{
			ResetGitRepo();

			RegisterFileContentsOnServer();

			HookAllSourceFiles();

		}

		private void HookAllSourceFiles()
		{
			foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
                AddHooksToSourceFile(sourceFile);
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

		private void AddHooksToSourceFile(SourceFile sourceFile)
		{
			var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();
			HooksRenderer hooksRenderer = new HooksRenderer(sourceFile, blocks);
            string outText = hooksRenderer.GetHookedCode();
			sourceFile.UpdateCodeContents(outText);
		}
	}
}
