using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class ClassInfoCollector: CSharpSyntaxWalker
    {
		public ClassInfo CollectedClassInfo { get; internal set; }
		int fieldIds = 0;
        int codeRunnerIds = 0;
		private SyntaxTree root;

		public ClassInfo Collect(ClassDeclarationSyntax node)
		{
			this.Visit(node);
			return CollectedClassInfo;
		}

		public ClassInfoCollector(SyntaxTree tree)
        {
			CollectedClassInfo = new ClassInfo();
			root = tree;
        }

		public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			int id = fieldIds;
			FieldInfoCollector collector = new FieldInfoCollector();
			FieldInfo fieldInfo = collector.Collect(node);
			CollectedClassInfo.AddFieldInfo(fieldInfo, id);
			fieldIds++;
			//base.VisitFieldDeclaration(node);
		}

		public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			int id = codeRunnerIds;
			CodeRunnerInfoCollector collector = new CodeRunnerInfoCollector(root);
			CodeRunnerInfo codeRunnerInfo = collector.Collect(node);
			CollectedClassInfo.AddCodeRunnerInfo(codeRunnerInfo, id);
			codeRunnerIds++;
			//base.VisitMethodDeclaration(node);
		}

		public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			int id = codeRunnerIds;
			CodeRunnerInfoCollector collector = new CodeRunnerInfoCollector(root);
            CodeRunnerInfo codeRunnerInfo = collector.Collect(node);
            CollectedClassInfo.AddCodeRunnerInfo(codeRunnerInfo, id);
			codeRunnerIds++;
			//base.VisitConstructorDeclaration(node);
		}

		public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			int id = codeRunnerIds;
			CodeRunnerInfoCollector collector = new CodeRunnerInfoCollector(root);
            CodeRunnerInfo codeRunnerInfo = collector.Collect(node);
            CollectedClassInfo.AddCodeRunnerInfo(codeRunnerInfo, id);
			codeRunnerIds++;
			//base.VisitAccessorDeclaration(node);
		}
	}
}
