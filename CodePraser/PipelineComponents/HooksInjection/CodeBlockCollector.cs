using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CodePraser.HooksInjection
{
    public class CodeBlockCollector : CSharpSyntaxWalker
    {
		private readonly SourceFile sourceFile;
		public List<CodeBlock> CodeBlocks;
		SyntaxTree tree;
		int blockId = 0;

		public CodeBlockCollector(SourceFile sourceFile, SyntaxTree pTree)
        {
			CodeBlocks = new List<CodeBlock>() { };
			this.sourceFile = sourceFile;
			this.tree = pTree;
			blockId = 0;
		}
		public override void VisitBlock(BlockSyntax node)
		{
			string methodName = "";
			bool isMethod = false;

			if(node.Parent is MethodDeclarationSyntax)
			{
				methodName = ((MethodDeclarationSyntax)node.Parent).Identifier.Text;
				isMethod = true;
			}

			if (node.Parent is ConstructorDeclarationSyntax)
            {
				methodName = ((ConstructorDeclarationSyntax)node.Parent).Identifier.Text;
                isMethod = true;
            }
			var cb = CreateCodeBlock(methodName, isMethod);

			for (int i = 0; i < node.Statements.Count; i++)
			{
				var statement = node.Statements[i];
				Statement s = CreateStatement(i, statement);
				cb.AddStatement(s);
			}

			CodeBlocks.Add(cb);
			blockId++;
			base.VisitBlock(node);
		}

		private Statement CreateStatement(int id, StatementSyntax statement)
		{
			int lineNo = GetLine(statement.Span);

			return new Statement(new Location(blockId, id) , lineNo );
		}

		private int GetLine(TextSpan span)
		{
			var lineNo = tree.GetLineSpan(span);
            int lno = lineNo.StartLinePosition.Line;
			return lno;
		}

		CodeBlock CreateCodeBlock(string methodName, bool isMethod)
		{
			var loc = new Location(blockId, 0);
			var cb = new CodeBlock(sourceFile, methodName, loc, isMethod);
			return cb;
		}
    }
}
