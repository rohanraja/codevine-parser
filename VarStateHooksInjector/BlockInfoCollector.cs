﻿using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;

public class BlockInfoCollector  : CSharpSyntaxWalker
{
	private Dictionary<int, List<StatementInfo>> blockInfo = new Dictionary<int, List<StatementInfo>>() { };
	SyntaxTree tree;
	int blockId = 0;

	public BlockInfoCollector(SyntaxTree root)
	{
		tree = root;
	}

	public Dictionary<int, List<StatementInfo>> Collect(BlockSyntax body)
	{
		this.Visit(body);
		return blockInfo;
	}

	public override void VisitBlock(BlockSyntax node)
    {

		List<StatementInfo> statementInfos = new List<StatementInfo>() { };

		for (int i = 0; i < node.Statements.Count; i++)
		{
			var statement = node.Statements[i];
			StatementInfo s = new StatementInfo();
			s.LineNo = GetLine(node.Statements[i].Span);
			statementInfos.Add(s);
        }

		blockInfo[blockId] = statementInfos;
        blockId++;
        base.VisitBlock(node);
    }


    private int GetLine(TextSpan span)
    {
        var lineNo = tree.GetLineSpan(span);
        int lno = lineNo.StartLinePosition.Line;
        return lno;
    }

}