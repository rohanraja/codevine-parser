using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

class FieldInfoCollector : CSharpSyntaxWalker
{
	internal FieldInfo Collect(FieldDeclarationSyntax node)
	{
		throw new NotImplementedException();
	}
}