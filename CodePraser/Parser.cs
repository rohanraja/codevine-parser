using CodePraser.HooksInjection;
using System;
namespace CodePraser
{
    public class Parser
    {
		private readonly SourceCodeInfo sourceCodeInfo;

		public Parser(SourceCodeInfo sourceCodeInfo)
        {
			this.sourceCodeInfo = sourceCodeInfo;
		}

        public void Parse()
		{
			ResetGitRepo();

			RegisterWithDB();

			foreach (var file in sourceCodeInfo.CodeFiles)
			{
				SourceFile sourceFile = new SourceFile(file, sourceCodeInfo.BaseDirPath);
				ParseFile(sourceFile);
			}
		}

		private void ResetGitRepo()
		{
			var git = new GitHelpers();
			git.ResetHard(sourceCodeInfo.BaseDirPath);
		}

		private void RegisterWithDB()
		{
			CodeRegisterer codeRegisterer = new CodeRegisterer();
			codeRegisterer.Register(sourceCodeInfo);
		}

		private void ParseFile(SourceFile sourceFile)
		{
			var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();
			HooksRenderer hooksRenderer = new HooksRenderer(sourceFile, blocks);
            string outText = hooksRenderer.GetHookedCode();
			sourceFile.UpdateCodeContents(outText);
		}
	}
}
