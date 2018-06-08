using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;
namespace VarStateHooksInjector
{
	public class CSFileInfoWriter :CSharpSyntaxRewriter
    {
		private readonly CSfileInfo cSfileInfo;
		int classIds = 0;

		public CSFileInfoWriter(CSfileInfo cSfileInfo)
		{
			this.cSfileInfo = cSfileInfo;
		}

		public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			int id = classIds;
			ClassInfoWriter writer = new ClassInfoWriter(cSfileInfo);
			classIds++;
			return writer.Generate(node, id);
			//return base.VisitClassDeclaration(node);
		}
	}
}
