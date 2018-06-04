using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class CodeRunnerBlockWriter
	{
		public static CodeRunnerBlockWriter GetWriter()
		{
			return new CodeRunnerBlockWriter();
		}

		public BlockSyntax RenderMethodInfo(CodeRunBlockRenderingInfo methodRenderingInfo, BlockSyntax blockSyntax)
		{
			return blockSyntax;
		}
	}
}
