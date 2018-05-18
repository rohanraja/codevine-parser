using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodePraser.HooksInjection
{
    public class CodeBlockCollector : CSharpSyntaxWalker
    {
		private readonly SourceFile sourceFile;
		public List<CodeBlock> CodeBlocks;
		int blockId = 0;

		public CodeBlockCollector(SourceFile sourceFile)
        {
			CodeBlocks = new List<CodeBlock>() { };
			this.sourceFile = sourceFile;
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
			AddCodeBlock(methodName, isMethod);
			blockId++;
			base.VisitBlock(node);
		}

        void AddCodeBlock(string methodName, bool isMethod)
		{
			var loc = new Location(blockId, 0);
			var cb = new CodeBlock(sourceFile, methodName, loc, isMethod);
			CodeBlocks.Add(cb);
		}
    }
}
