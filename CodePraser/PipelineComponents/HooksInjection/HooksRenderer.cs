using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodePraser.HooksInjection
{
    public class HooksRenderer
    {
		private readonly SourceFile sourceFile;
		private readonly List<CodeBlock> codeBlocks;
		private SyntaxTree syntaxTree;

		public HooksRenderer(SourceFile sourceFile, List<CodeBlock> codeBlocks)
        {
			this.sourceFile = sourceFile;
			this.codeBlocks = codeBlocks;
			Parse();

		}
		void Parse()
        {
            syntaxTree = CSharpSyntaxTree.ParseText(sourceFile.GetCode());
        }

        CompilationUnitSyntax GetRoot()
        {

            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
            return root;
        }

		public string GetHookedCode()
		{
			var root = GetRoot();
			var statementWriter = new StatementWriter(codeBlocks);
			var newRoot = statementWriter.Visit(root);
			return newRoot.GetText().ToString();
		}
	}
}
