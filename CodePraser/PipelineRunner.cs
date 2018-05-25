using System;
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

		public string ProjectPath { get; }
		public string ProjectFileName { get; }

		public void RunPipeLine()
		{

			log.Debug("Instantiating Pipleline Dependencies");
			var sourceHooker = new SourceFileHooker();
			var git = new GitHelpers();
			var projectReferenceInjector = new ProjectReferenceInjector();
			var codeRegisterer = new CodeRegisterer();
			var projParser = new BuildalyzerProjectParser();

			ProjectHookingPipeline parser = new ProjectHookingPipeline(sourceHooker, git, projectReferenceInjector, codeRegisterer, projParser);
			parser.Run(ProjectPath, ProjectFileName);
		}
	}
}
