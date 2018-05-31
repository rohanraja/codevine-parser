using System;
using CodeParserCommon;
namespace CodePraser
{
	public class ProjectHookingPipeline
    {
		private ISourceFileHooker sourceFileHooker;
		private IProjectReferenceInjector projectReferenceInjector;
		private ICodeRegisterer codeRegisterer;
		private IGitHelpers git;
		private IProjectParser projParser;
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ProjectHookingPipeline(
			ISourceFileHooker sourceHooker,
			IGitHelpers git_h,
			IProjectReferenceInjector refInject,
			ICodeRegisterer codeReg,
			IProjectParser projParserP
		)
		{
			sourceFileHooker = sourceHooker;
			git = git_h;
			projectReferenceInjector = refInject;
			codeRegisterer = codeReg;
			projParser = projParserP;
		}
        
		public void Run(string projectPath, string projectFileName)
		{
			log.Info(new { projectPath, projectFileName });

            var sourceCodeInfo = projParser.GetSourceCodeInfo(projectPath, projectFileName);

            // git.ResetHard(sourceCodeInfo.BaseDirPath);

            projectReferenceInjector.InjectReference(sourceCodeInfo, projectFileName);

            //codeRegisterer.SendCodeContentsToServer(sourceCodeInfo);

            //sourceFileHooker.AddHooksToSourceCode(sourceCodeInfo);

        }

    }
}
