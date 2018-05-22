using System;
using System.Collections.Generic;
using CodePraser.PipelineComponents.HooksInjection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using CodeParserCommon;

namespace CodePraser.HooksInjection
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
			var codeblock = getCurrentBlock();

			if (codeblock == null)
			{
				blockId++;
				return base.VisitBlock(node);
			}

			var newStments = CreateNewStatements(codeblock, node.Statements);
			blockId++;
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
        
		Hooks getCurrentBlock()
		{
			if (blockId >= hooksList.Count)
				return null;
			return hooksList[blockId];
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
