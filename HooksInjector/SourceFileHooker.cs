using CodeParserCommon;

namespace HooksInjector
{
    public class SourceFileHooker : ISourceFileHooker
	{
		private HookInjectionPipeline hookInjectionPipeline;

		public SourceFileHooker()
		{
			hookInjectionPipeline = new HookInjectionPipeline();
		}

		public void AddHooksToSourceCode(SourceCodeInfo sourceCodeInfo)
		{

            foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
				var outText = hookInjectionPipeline.AddHooksToSourceFile(sourceFile);
				sourceFile.UpdateCodeContents(outText);
            }
		}

    }
}
