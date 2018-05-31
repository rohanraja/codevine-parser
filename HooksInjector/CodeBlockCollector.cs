using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using CodeParserCommon;

namespace HooksInjector
{
    public class CodeBlockCollector : CSharpSyntaxWalker
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

			log.DebugFormat("Block Parent type: {0}, at blockId {1}", node.Parent.GetType().Name, blockId);

			if(node.Parent is MethodDeclarationSyntax)
			{
				methodName = ((MethodDeclarationSyntax)node.Parent).Identifier.Text;
				isMethod = true;
				log.Debug(new { methodName });
			}

			if (node.Parent is ConstructorDeclarationSyntax)
            {
				methodName = ((ConstructorDeclarationSyntax)node.Parent).Identifier.Text;
                isMethod = true;
				log.Debug(new { methodName });
            }
			var cb = CreateCodeBlock(methodName, isMethod);

			for (int i = 0; i < node.Statements.Count; i++)
			{
				var statement = node.Statements[i];
				Statement s = CreateStatement(i, statement, node.Statements.Count, isMethod);
				log.DebugFormat("Creating Statement {0}", i);
				cb.AddStatement(s);
			}

			CodeBlocks.Add(cb);
			blockId++;
			base.VisitBlock(node);
		}

		private Statement CreateStatement(int id, StatementSyntax statement, int count, bool isMethod)
		{
			int lineNo = GetLine(statement.Span);
			string methodRunningState = "RUNNING";
			if (id == 0 && isMethod)
				methodRunningState += ",ENTERED";
			// ToDo: Extract constants like "ENTERED" to global vars

			if (id == count-1 && isMethod)
                methodRunningState += ",EXITING";
			// ToDo: Check exitied for exception, return statements in ifs also

			return new Statement(new Location(blockId, id) , lineNo, methodRunningState );
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
