using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodeParserCommon;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CodeParserTests
{
    [TestClass]
    public class CSFilesLister_Tests
    {

        [TestMethod]
        public void TestBuildAlayzerLister()
        {
            ICSFilesLister lister = new BuildAlyzerLister();
            var expectedFIles = new List<string> { "Program.cs", "ClassA.cs", "ClassB.cs" };
			RunListerTests(lister, GitTests.FullProjPath, expectedFIles);
        }

        [TestMethod]
        public void TestBuildAlayzerListerOnSales()
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (!isWindows)
                return;

            ICSFilesLister lister = new CRM_Solutions_CSFileLister_Decorator();
            var expectedFIles = new List<string> {};

            string projPath = GitTests.SalesPluginProjPath;

			RunListerTests(lister, projPath, expectedFIles, GitTests.SalesPluginProjCSFilesCount);
        }

        private void RunListerTests(ICSFilesLister lister, string projPath, List<string> expectedFiles, int expCount = -1)
        {
            var csFiles = lister.GetCSCodeFiles(projPath);


            foreach(var expFile in expectedFiles)
            {
                Assert.IsTrue(csFiles.Contains(expFile));
            }

            if(expCount == -1)
            {
                Assert.IsTrue(csFiles.Count == expectedFiles.Count);
                foreach(var expFile in csFiles)
                {
                    Assert.IsTrue(expectedFiles.Contains(expFile));
                }

            }else
            {

                Assert.IsTrue(csFiles.Count == expCount);
            }

        }

    }
}
