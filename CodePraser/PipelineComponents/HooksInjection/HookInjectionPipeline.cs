using System;
using CodePraser.HooksInjection;
using CodeParserCommon;

namespace CodePraser.PipelineComponents.HooksInjection
{
    public class HookInjectionPipeline
    {
        public HookInjectionPipeline()
        {
        }

		public void AddHooksToSourceFile(SourceFile sourceFile)
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
