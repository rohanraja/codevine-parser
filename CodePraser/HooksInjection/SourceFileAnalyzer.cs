using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodePraser.HooksInjection
{
	public class SourceFileAnalyzer
    {
		private SyntaxTree syntaxTree;

		public SourceFile SourceFile { get; }

		public SourceFileAnalyzer(SourceFile sourceFile)
        {
			SourceFile = sourceFile;
			Parse();
		}

        void Parse()
		{
			syntaxTree = CSharpSyntaxTree.ParseText(SourceFile.GetCode());

		}

		CompilationUnitSyntax GetRoot()
		{
			
			CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
			return root;
		}

		public List<CodeBlock> GetCodeBlocks()
		{
			var codeBlockCollector = new CodeBlockCollector(this.SourceFile, this.syntaxTree);

			var root = GetRoot();

			codeBlockCollector.Visit(root);

			return codeBlockCollector.CodeBlocks;
		}

	}
}
