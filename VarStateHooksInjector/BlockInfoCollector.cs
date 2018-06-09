using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;

public class BlockInfoCollector  : CSharpSyntaxRewriter
{
	private Dictionary<int, List<StatementInfo>> blockInfo = new Dictionary<int, List<StatementInfo>>() { };

	public Dictionary<int, List<StatementInfo>> Collect(BlockSyntax body)
	{
		this.Visit(body);
		return blockInfo;
	}
}