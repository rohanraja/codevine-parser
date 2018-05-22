using CodePraser.PipelineComponents.HooksInjection;

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

		// Todo : Refactor pipline to interface
	    void AddHooksToSourceFile(SourceFile sourceFile)
        {
            var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);
            var blocks = sourceFileAnalyzer.GetCodeBlocks();

			CodeblocksToHooksGenerator gen = new CodeblocksToHooksGenerator();
            var hooksList = gen.GenerateHooks(blocks);

            HooksRenderer hooksRenderer = new HooksRenderer();
            string outText = hooksRenderer.GetHookedCode(sourceFile, hooksList);

			sourceFile.UpdateCodeContents(outText);
        }

    }
}
