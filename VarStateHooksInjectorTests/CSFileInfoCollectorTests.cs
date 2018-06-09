using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using System.Collections.Generic;

namespace VarStateHooksInjectorTests
{
	[TestClass]
    public class CSFileInfoCollectorTests
    {
		[TestMethod]
        public void TestSimple2LineMethodClass()
		{
			TestCase testCase = TestCase.GetSimple2LineMethodCase();
			RunCsFileInfoCollectorTest(testCase);
		}

		[TestMethod]
        public void TestSingleIfMethod()
        {
			TestCase testCase = TestCase.GetSingleIfMethod();
            RunCsFileInfoCollectorTest(testCase);
        }

		private void RunCsFileInfoCollectorTest(TestCase testCase)
		{
			var collector = new CSFileInfoCollector(testCase.Root, testCase.FileName);
			CSfileInfo cSfileInfo = collector.Collect(testCase.Root.GetRoot());

			Assert.IsTrue(cSfileInfo.Classes.Count > 0);
			Assert.IsTrue(cSfileInfo.Classes[0].CodeRunners[0].Name == testCase.ClassInfo.CodeRunners[0].Name);


		}
	}
}
