using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using CodeParserCommon;

namespace HooksInjector
{
	public class StatementWriter : CSharpSyntaxRewriter
    {
		int blockId = 0;
		private List<Hooks> hooksList;

		public StatementWriter(List<Hooks> hooksList)
		{
			this.hooksList = hooksList;
		}

		public override SyntaxNode VisitBlock(BlockSyntax node)
		{
			int thisBlockId = blockId + 0;
			blockId++;
			node = base.VisitBlock(node) as BlockSyntax ;

			var codeblock = getCurrentBlock(thisBlockId);

			if (codeblock == null)
			{
				return base.VisitBlock(node);
			}

			var newStments = CreateNewStatements(codeblock, node.Statements);
			return node.WithStatements(newStments);
		}

		private SyntaxList<StatementSyntax> CreateNewStatements(Hooks codeblock, SyntaxList<StatementSyntax> statements)
		{
			List<KeyValuePair<int, string>> hooks = codeblock.Pairs;

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
        
		Hooks getCurrentBlock(int bid)
		{
			if (bid >= hooksList.Count)
				return null;
			return hooksList[bid];
		}

		public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
		{
			var doneNode = base.VisitCompilationUnit(node);
			var usng = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" CodeRecordHelpers"));
			var newUsings = node.Usings.Add(usng);
			return ((CompilationUnitSyntax)doneNode).WithUsings(newUsings);
		}
	}
}
