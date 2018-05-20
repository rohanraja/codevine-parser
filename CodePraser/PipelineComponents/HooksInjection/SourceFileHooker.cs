namespace CodePraser.HooksInjection
{
    public class SourceFileHooker : ISourceFileHooker
	{
		public void AddHooksToSourceCode(SourceCodeInfo sourceCodeInfo)
		{
            foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
                AddHooksToSourceFile(sourceFile);
            }
		}

	    void AddHooksToSourceFile(SourceFile sourceFile)
        {
            var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();
            HooksRenderer hooksRenderer = new HooksRenderer(sourceFile, blocks);
            string outText = hooksRenderer.GetHookedCode();
            sourceFile.UpdateCodeContents(outText);
        }

    }
}
