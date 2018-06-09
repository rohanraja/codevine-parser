using CodeParserCommon;

namespace VarStateHooksInjector
{
    public class VarStateSourceFileHooker : ISourceFileHooker
	{
		private HookInjectionPipeline hookInjectionPipeline;

		public VarStateSourceFileHooker()
		{
			hookInjectionPipeline = new HookInjectionPipeline();
		}

		public void AddHooksToSourceCode(SourceCodeInfo sourceCodeInfo)
		{

            foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
				string contents = sourceFile.GetCode();
				string fileName = sourceFile.FilePath;

				string outText = hookInjectionPipeline.AddHooksToSourceFile(fileName, contents);
				sourceFile.UpdateCodeContents(outText);
            }
		}

    }
}
