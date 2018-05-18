using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CodePraser.HooksInjection
{
	public class StatementWriter : CSharpSyntaxRewriter
    {
		private readonly List<CodeBlock> codeBlocks;
		int blockId = 0;

		public StatementWriter(List<CodeBlock> codeBlocks)
		{
			this.codeBlocks = codeBlocks;
		}

		public override SyntaxNode VisitBlock(BlockSyntax node)
		{
			var codeblock = getCurrentBlock();

			var newStments = CreateNewStatements(codeblock, node.Statements);
			blockId++;
			return node.WithStatements(newStments);
		}

		private SyntaxList<StatementSyntax> CreateNewStatements(CodeBlock codeblock, SyntaxList<StatementSyntax> statements)
		{
			List<KeyValuePair<int, string>> hooks = codeblock.GetHooks();

			var outStatements = statements;
            
			int added = 0;
			foreach(var hook in hooks)
			{
				var newStmt = SyntaxFactory.ParseStatement(hook.Value);
				outStatements = outStatements.Insert(hook.Key + added, newStmt);
				added++;
			}
			return outStatements;
		}

		CodeBlock getCurrentBlock()
		{
			return codeBlocks[blockId];
		}
	}
}
