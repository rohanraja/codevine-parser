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
			var newClassNode = writer.Generate(node, id) as ClassDeclarationSyntax;
			var newProps = HookedProperties(id);

			classIds++;

			return newClassNode.WithMembers(newClassNode.Members.AddRange(newProps));
		}

		public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            var doneNode = base.VisitCompilationUnit(node);
            var usng = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" CodeRecordHelpers"));
            var newUsings = node.Usings.Add(usng);
            return ((CompilationUnitSyntax)doneNode).WithUsings(newUsings);
        }

		private SyntaxList<MemberDeclarationSyntax> HookedProperties(int id)
		{
			SyntaxList<MemberDeclarationSyntax> outp = new SyntaxList<MemberDeclarationSyntax>();

			PropertyWithValueListenerGen gen = new PropertyWithValueListenerGen();
			var classInfo = cSfileInfo.GetClassInfo(id);
			foreach(var key in classInfo.FieldInfos.Keys)
			{
				var finfo = classInfo.FieldInfos[key];

                // Todo - fix sending classInfo name directly to prop gen
				var newProp = gen.GenerateProperty(finfo, classInfo.Name);

				outp = outp.AddRange(newProp);
			}
			return outp;
		}
	}
}
