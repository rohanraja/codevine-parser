using System;
using CodeParserCommon;

namespace HooksInjector
{
    public class HookInjectionPipeline
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HookInjectionPipeline()
        {
        }

		public string AddHooksToSourceFile(SourceFile sourceFile)
        {
			log.Info(new { sourceFile.FilePath });
            
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
