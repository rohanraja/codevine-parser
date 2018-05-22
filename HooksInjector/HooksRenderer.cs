using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CodeParserCommon;

namespace HooksInjector
{
    public class HooksRenderer
    {
		private SyntaxTree syntaxTree;

        public HooksRenderer()
        {
        }

		void Parse(SourceFile sourceFile)
        {
            syntaxTree = CSharpSyntaxTree.ParseText(sourceFile.GetCode());
        }

        CompilationUnitSyntax GetRoot()
        {

            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
            return root;
        }

        public string GetHookedCode(SourceFile sourceFile, List<Hooks> hooksList)
        {
			Parse(sourceFile);
			var root = GetRoot();
			var statementWriter = new StatementWriter(hooksList);
            var newRoot = statementWriter.Visit(root);
            return newRoot.GetText().ToString();
        }
   	}
}
