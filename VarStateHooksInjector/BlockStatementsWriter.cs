using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	internal class BlockStatementsWriter : CSharpSyntaxRewriter
    {
		private readonly CodeRunBlockRenderingInfo info;
		int blockId = 0;

		public BlockStatementsWriter(CodeRunBlockRenderingInfo info)
        {
			this.info = info;
		}

		public override SyntaxNode VisitBlock(BlockSyntax node)
        {
            int thisBlockId = blockId + 0;
            blockId++;
            node = base.VisitBlock(node) as BlockSyntax;

            var newStatementInfos = getCurrentBlock(thisBlockId);

			if (newStatementInfos == null)
            {
                return base.VisitBlock(node);
            }

			var newStments = CreateNewStatements(newStatementInfos, node.Statements);
            return node.WithStatements(newStments);
        }

        private SyntaxList<StatementSyntax> CreateNewStatements(List<string> statinfos, SyntaxList<StatementSyntax> statements)
        {
			var outStatements = new SyntaxList<StatementSyntax>() { };

			foreach (var statinfo in statinfos)
            {
				if(IsOriginalStatement(statinfo))
				{
					int origId = int.Parse(statinfo);
					var newStment = statements[origId];
					outStatements = outStatements.Add(newStment);
				}
				else
				{
					var newStmt = SyntaxFactory.ParseStatement(statinfo);
					outStatements = outStatements.Add(newStmt);
				}
            }
            return outStatements;
        }

		private bool IsOriginalStatement(string statinfo)
		{
			int tmpVal;
			return int.TryParse(statinfo, out tmpVal);
		}

		List<string> getCurrentBlock(int bid)
        {
            if (! info.ContainsId(bid))
                return null;

            return info.GetStatementsForId(bid);
        }
	}
}
