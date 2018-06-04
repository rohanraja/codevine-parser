using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class CodeRunnerBlockRenderer 
	{

		public static CodeRunnerBlockRenderer GetWriter()
		{
			return new CodeRunnerBlockRenderer();
		}

		public BlockSyntax RenderMethodInfo(CodeRunBlockRenderingInfo methodRenderingInfo, BlockSyntax blockSyntax)
		{
			BlockStatementsWriter blockStatementsWriter = new BlockStatementsWriter(methodRenderingInfo);
			var newBlock = blockStatementsWriter.Visit(blockSyntax) as BlockSyntax;
			return newBlock;
		}
	}
}
