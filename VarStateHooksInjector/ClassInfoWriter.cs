using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class ClassInfoWriter : CSharpSyntaxRewriter
    {
		private ClassInfo classInfo;
		int fieldIds = 0;
		int codeRunnerIds = 0;
		private CSfileInfo cSfileInfo;

		internal SyntaxNode Generate(ClassDeclarationSyntax node, int id)
        {
			classInfo = cSfileInfo.GetClassInfo(id);
			return this.Visit(node);
        }

		public ClassInfoWriter(CSfileInfo cSfileInfo)
		{
			this.cSfileInfo = cSfileInfo;
		}

		public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			int id = fieldIds;
			FieldGenerator fieldGenerator = new FieldGenerator(classInfo);
			FieldDeclarationSyntax newField = fieldGenerator.Generate(node, id);
			fieldIds++;
			return newField;
			//return base.VisitFieldDeclaration(node);
		}


		public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			int id = codeRunnerIds;

			HookedRenderInfoGenerator generator = new HookedRenderInfoGenerator();
            CodeRunBlockRenderingInfo renderinfo = generator.CodeRunBlockRenderInfoForMethod(classInfo, id);
            CodeRunnerBlockRenderer renderer = CodeRunnerBlockRenderer.GetWriter();
            var newBlock = renderer.RenderMethodInfo(renderinfo, node.Body);

			codeRunnerIds++;
            return node.WithBody(newBlock);
			//return base.VisitMethodDeclaration(node);
		}

		public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
			int id = codeRunnerIds;

            HookedRenderInfoGenerator generator = new HookedRenderInfoGenerator();
			CodeRunBlockRenderingInfo renderinfo = generator.CodeRunBlockRenderInfoForConstructor(classInfo, id);
            CodeRunnerBlockRenderer renderer = CodeRunnerBlockRenderer.GetWriter();
            var newBlock = renderer.RenderMethodInfo(renderinfo, node.Body);

            codeRunnerIds++;
            return node.WithBody(newBlock);

            //return base.VisitMethodDeclaration(node);
        }
	}
}
