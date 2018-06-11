using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace VarStateHooksInjector
{
	
    public class FieldInfoCollector : CSharpSyntaxWalker
    {
    	public FieldInfo Collect(FieldDeclarationSyntax node)
    	{
    		//throw new NotImplementedException();
    		var finfo = new FieldInfo();
			finfo.Name = node.Declaration.Variables[0].Identifier.ToString();
			finfo.Type = node.Declaration.Type;
			finfo.Modifiers = node.Modifiers;
			return finfo;
    	}
    }
}