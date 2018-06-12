using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace VarStateHooksInjector{
	
    class CodeRunnerInfoCollector
    {
    	SyntaxTree root;

    	public CodeRunnerInfoCollector(SyntaxTree tree)
    	{
    		this.root = tree;
    	}

    	internal CodeRunnerInfo Collect(MethodDeclarationSyntax node)
    	{
			return collectInfo(node, node.Identifier.ToString(), false);
    	}

    	internal CodeRunnerInfo Collect(ConstructorDeclarationSyntax node)
    	{
			return collectInfo(node, node.Identifier.ToString(), true);
    	}

        internal CodeRunnerInfo Collect(AccessorDeclarationSyntax node)
        {
            // Todo - Add unit tests for this

            var info = new CodeRunnerInfo();

			try
			{
				info.Name = getAccessorName(node);
			}
			catch
			{
				
			}

			// Collect all the statements and block infos
			info.blockInfo = collectBlocks(node.Body);

			// Check if the method is static
            info.IsStatic = CheckForStatic(node.Modifiers);

			return info;
        }


		private CodeRunnerInfo collectInfo(BaseMethodDeclarationSyntax node, string name, bool isConstructor = false )
		{
			
			var info = new CodeRunnerInfo();

			info.Name = name;
			info.IsConstructor = isConstructor;

            // Collect all the statements and block infos
			info.blockInfo = collectBlocks(node.Body);

			// Check if the method is static
			info.IsStatic = CheckForStatic(node.Modifiers);

			// Collect all method arguments
			info.Arguments = CollectArguments(node.ParameterList);

            return info;
		}

		private List<MethodArgument> CollectArguments(ParameterListSyntax parameterList)
		{
			List<MethodArgument> outp = new List<MethodArgument>(){} ;
			foreach (var param in parameterList.Parameters)
            {
                var methArg = new MethodArgument { Name = param.Identifier.ToString() };
                outp.Add(methArg);
            }
			return outp;
		}

		private bool CheckForStatic(SyntaxTokenList modifiers)
		{
			foreach (var mod in modifiers)
            {
				if (mod.Text.ToLower().Contains("static"))
					return true;
            }
			return false;
		}

		private Dictionary<int, BlockInfo> collectBlocks(BlockSyntax body)
		{
			BlockInfoCollector blockInfo = new BlockInfoCollector(root);
            return blockInfo.Collect(body);
		}

		private string getAccessorName(AccessorDeclarationSyntax par)
        {
            string methodName = "";
            var attrPar = par.Parent.Parent as PropertyDeclarationSyntax;
            string attrName = attrPar.Identifier.Text;
            string mtype = "set";
            if (par.Kind().ToString().ToLower().Contains("get"))
                mtype = "get";
            methodName = attrName + "." + mtype;
            return methodName;
        }

	}
}
