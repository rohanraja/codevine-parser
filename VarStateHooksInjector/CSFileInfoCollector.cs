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
		List<ClassInfo> classes = new List<ClassInfo>() { };

        public CSFileInfoCollector()
        {
        }

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			ClassInfoCollector collector = new ClassInfoCollector();
			ClassInfo classInfo = collector.Collect(node);
			classes.Add(classInfo);
			//base.VisitClassDeclaration(node);
		}

	}
}
