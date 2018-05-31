using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using System;
using CodeParserCommon;
using System.IO;

namespace CodeParserTests
{
	[TestClass]
    public class GitTests
    {
        public static string RepoCsProj = "TestCSharpProject.csproj";
        private static string parentPath;
        
        public static string SalesPluginProjPath = @"C:\src\CRM.Solutions.Sales\solutions\Sales\Plugins\SalesPlugins.csproj";
        internal static int SalesPluginProjCSFilesCount = 126;

        public static string RepoPath {
            get
            {
                parentPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                return Path.Combine(parentPath, "TestCSharpProject");
            }
        }
        public static string FullProjPath {
            get
            {
                return Path.Combine(RepoPath, RepoCsProj);
            }
        }
	}
}
