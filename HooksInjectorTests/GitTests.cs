using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodeParserCommon;

namespace HooksInjectorTests
{
	[TestClass]
    public class GitTests
    {
		public static string RepoPath = @"C:\src\codevine-parser\TestCSharpProject";

		public static string RepoCsProj = "TestCSharpProject.csproj";


		internal static SourceCodeInfo GetSourceCode()
		{
			throw new NotImplementedException();
		}
	}
}
