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
			var newClassNode = writer.Generate(node, id) as ClassDeclarationSyntax;

			return newClassNode.WithMembers(newClassNode.Members);
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
