using System;
using CodePraser;

namespace CodeVine_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Arming Project for Code Recording");

            string RepoPath = @"C:\src\TestCodeVine1\TestCodeVine1";

            string RepoCsProj = "TestCodeVine1.csproj";

			if(args.GetLength(0) > 0)
			{
				RepoPath = args[0];
				RepoCsProj = args[1];
			}

			var piplineRunner = new PipelineRunner(RepoPath, RepoCsProj);
			piplineRunner.RunPipeLine();
        }
    }
}
