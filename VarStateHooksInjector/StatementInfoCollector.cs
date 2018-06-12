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

			var exprNode = node as ExpressionStatementSyntax;
			if(exprNode != null)
			{

				IdentifierNameSyntax idst = null;

				var expr = exprNode.Expression as AssignmentExpressionSyntax;
                if(expr != null)
				{
                    idst = expr.Left as IdentifierNameSyntax;
				}

				var postExpr = exprNode.Expression as PostfixUnaryExpressionSyntax;
				if (postExpr != null)
                {
					idst = postExpr.Operand as IdentifierNameSyntax;
                }

                // Todo - Ensure there are no more cases for local var identification.

				if (idst != null)
                {
                    var name = idst.Identifier.ToString();
                    info.LocalVarNames.Add(name);
                    info.IsLocalVarStateChanger = true;

                }
                
			}


			return info;
    	}
    }
}