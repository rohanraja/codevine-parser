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
			foreach(var var in node.Declaration.Variables)
			{
				string fname = var.Identifier.ToString();
				finfo.Names.Add(fname);
				
			}
			finfo.Type = node.Declaration.Type;
			finfo.Modifiers = node.Modifiers;
			return finfo;
    	}
    }
}