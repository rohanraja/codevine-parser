using System;
using CodePraser;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace CodeVine_Parser
{
    class Program
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            string RepoPath = @"C:\src\TestCodeVine2\Plugins";

            string RepoCsProj = "ProductManagementPlugins.csproj";

			if(args.GetLength(0) > 0)
			{
				RepoPath = args[0];
				RepoCsProj = args[1];
			}

			log.Info("Parsing Project for Code Recording");
			log.Info(new {RepoPath, RepoCsProj});

			var piplineRunner = new PipelineRunner(RepoPath, RepoCsProj);
			piplineRunner.RunPipeLine();
        }
    }
}
