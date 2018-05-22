using System;
using CodeParserCommon;

namespace HooksInjector
{
    public class HookInjectionPipeline
    {
        public HookInjectionPipeline()
        {
        }

		public string AddHooksToSourceFile(SourceFile sourceFile)
        {
            var sourceFileAnalyzer = new SourceFileAnalyzer(sourceFile);
            var blocks = sourceFileAnalyzer.GetCodeBlocks();

            CodeblocksToHooksGenerator gen = new CodeblocksToHooksGenerator();
            var hooksList = gen.GenerateHooks(blocks);

            HooksRenderer hooksRenderer = new HooksRenderer();
            string outText = hooksRenderer.GetHookedCode(sourceFile, hooksList);
			return outText;

        }
    }
}
