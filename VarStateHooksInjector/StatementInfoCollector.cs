using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace VarStateHooksInjector
{
	
    public class StatementInfoCollector : CSharpSyntaxWalker
    {
		public StatementInfo Collect(StatementSyntax node)
    	{

			StatementInfo info = new StatementInfo();

			var localDecStx = node as LocalDeclarationStatementSyntax;
			if(localDecStx != null)
			{
				info.IsLocalVarDeclaration = true;
				foreach(var varDec in localDecStx.Declaration.Variables)
				{
					info.LocalVarNames.Add(varDec.Identifier.ToString());
				}

				return info;
			}


			return info;
    	}
    }
}