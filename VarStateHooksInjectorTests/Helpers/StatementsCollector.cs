using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace VarStateHooksInjectorTests
{
	public class StatementsCollector : CSharpSyntaxWalker
    {
        public List<StatementSyntax> Statements = new List<StatementSyntax>() { };

		public override void VisitBlock(BlockSyntax node)
		{
			Statements.AddRange(node.Statements);
			base.VisitBlock(node);
		}

	}
}
