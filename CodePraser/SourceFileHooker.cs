using CodePraser.HooksInjection;
namespace CodePraser
{
    public class SourceFileHooker
    {
        public void AddHooksToSourceFile(SourceFile sourceFile)
        {
            var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);

            var blocks = sourceFileAnalyzer.GetCodeBlocks();
            HooksRenderer hooksRenderer = new HooksRenderer(sourceFile, blocks);
            string outText = hooksRenderer.GetHookedCode();
            sourceFile.UpdateCodeContents(outText);
        }

    }
}
