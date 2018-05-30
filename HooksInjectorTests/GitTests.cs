using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodeParserCommon;
using System.IO;

namespace HooksInjectorTests
{
	[TestClass]
    public class GitTests
    {
        public static string RepoCsProj = "TestCSharpProject.csproj";
        private static string parentPath;

        public static string RepoPath {
            get
            {
                parentPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                return Path.Combine(parentPath, "TestCSharpProject");
            }
        }

	}
}
