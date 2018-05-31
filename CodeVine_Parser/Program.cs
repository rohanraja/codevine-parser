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
            string RepoCsProj = @"C:\src\CRM.Solutions.Sales\solutions\Sales\Plugins\SalesPlugins.csproj";

			if(args.GetLength(0) > 0)
			{
				RepoCsProj = args[0];
			}

			log.Info("Parsing Project for Code Recording");
			log.Info(new {RepoCsProj});

			var piplineRunner = new PipelineRunner(RepoCsProj);
			piplineRunner.RunPipeLine();
        }
    }
}
