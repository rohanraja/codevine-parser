using CodeParserCommon;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace HooksInjector
{
    public class SourceFileHooker : ISourceFileHooker
	{
		private HookInjectionPipeline hookInjectionPipeline;

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public SourceFileHooker()
		{
			hookInjectionPipeline = new HookInjectionPipeline();
		}

		public void AddHooksToSourceCode(SourceCodeInfo sourceCodeInfo)
		{
			log.Info(new { sourceCodeInfo.BaseDirPath });

            foreach (var sourceFile in sourceCodeInfo.SourceFiles)
            {
				var outText = hookInjectionPipeline.AddHooksToSourceFile(sourceFile);
				sourceFile.UpdateCodeContents(outText);
            }
		}

    }
}
