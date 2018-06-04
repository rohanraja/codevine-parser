using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VarStateHooksInjectorTests
{
    public class Helpers
    {
        public Helpers()
        {
        }

		internal static MethodDeclarationSyntax ParseMethodSyntax(string testMethod)
		{
			var rt = SyntaxFactory.ParseSyntaxTree(testMethod).GetRoot();
            MethodDeclarationSyntax methSyntax = rt.ChildThatContainsPosition(0).AsNode() as MethodDeclarationSyntax;
			return methSyntax;
		}
	}
}
