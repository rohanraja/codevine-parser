﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector;
using VarStateHooksInjector.Entities;
using System.Collections.Generic;

namespace VarStateHooksInjectorTests
{
	[TestClass]
    public class BlockInfoCollectorTests
    {
		[TestMethod]
		public void TestForSimple2LineBlock()
        {
			TestCase testCase = TestCase.GetSimple2LineMethodCase();
			var block = Helpers.GetFirstNodeOfType<BlockSyntax>(testCase.Code);

			BlockInfoCollector collector = new BlockInfoCollector(testCase.Root);
			var blockInfo = collector.Collect(block);

			var expectedBlockInfo = testCase.ClassInfo.GetCodeRunnerInfo(0).blockInfo;
			CheckBlockInfoMatches(blockInfo, expectedBlockInfo);

        }

		private void CheckBlockInfoMatches(Dictionary<int, List<StatementInfo>> blockInfo, Dictionary<int, List<StatementInfo>> expectedBlockInfo)
		{
			Assert.IsTrue(blockInfo.Keys.Count == expectedBlockInfo.Keys.Count);


			foreach(var key in expectedBlockInfo.Keys)
			{
				Assert.IsTrue(blockInfo[key].Count == expectedBlockInfo[key].Count);

				for (int i = 0; i < expectedBlockInfo[key].Count; i++)
				{
					var s1 = blockInfo[key][i];
					var s2 = expectedBlockInfo[key][i];
					CheckStatementsMatch(s1, s2);
				}
			}

		}

		private void CheckStatementsMatch(StatementInfo s1, StatementInfo s2)
		{
			Assert.IsTrue(s1.LineNo == s2.LineNo);
		}
	}
}
