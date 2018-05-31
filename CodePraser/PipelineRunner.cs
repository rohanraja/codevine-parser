using System;
using System.IO;
using System.Runtime.InteropServices;
using CodeParserCommon;
using HooksInjector;

namespace CodePraser
{
    public class PipelineRunner
    {

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public PipelineRunner(string projectPath, string projectFileName)
        {
			ProjectPath = projectPath;
			ProjectFileName = projectFileName;
		}
		public PipelineRunner(string fullProjPath)
        {
			ProjectPath = Path.GetDirectoryName(fullProjPath);
			ProjectFileName = Path.GetFileName(fullProjPath);
		}

		public string ProjectPath { get; }
		public string ProjectFileName { get; }

		public void RunPipeLine()
		{
			bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

			log.Debug("Instantiating Pipleline Dependencies");
			var sourceHooker = new SourceFileHooker();
			var git = new GitHelpers();

			IProjectReferenceInjector projectReferenceInjector = new ProjectReferenceInjector();


			if (!isWindows)
				projectReferenceInjector = new MacDevReferenceInjector();

			var codeRegisterer = new CodeRegisterer();
			var projParser = new BuildalyzerProjectParser();

			ProjectHookingPipeline parser = new ProjectHookingPipeline(sourceHooker, git, projectReferenceInjector, codeRegisterer, projParser);
			parser.Run(ProjectPath, ProjectFileName);
		}
	}
}
