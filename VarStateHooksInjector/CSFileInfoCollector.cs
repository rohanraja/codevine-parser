using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
    // Todo - Test that for multiple classes in one files, this visit is called! 
	public class CSFileInfoCollector : CSharpSyntaxWalker
    {
		CSfileInfo cSfileInfo;
		private SyntaxTree root;
		private readonly string fileName;

		public CSFileInfoCollector(SyntaxTree tree, string fileName)
        {
			root = tree;
			this.fileName = fileName;
		}

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			ClassInfoCollector collector = new ClassInfoCollector(root);
			ClassInfo classInfo = collector.Collect(node);
			classInfo.RelativeFilePath = fileName;
			cSfileInfo.Classes.Add(classInfo);
			//base.VisitClassDeclaration(node);
		}

		public CSfileInfo Collect(SyntaxNode syntaxNode)
		{
			cSfileInfo = new CSfileInfo();
			this.Visit(syntaxNode);
			return cSfileInfo;
		}
	}
}
