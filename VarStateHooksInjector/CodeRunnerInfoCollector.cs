using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;
using Microsoft.CodeAnalysis;

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
            // Todo - implement this
            return new CodeRunnerInfo();
        }

		private CodeRunnerInfo collectInfo(BaseMethodDeclarationSyntax node, string name, bool isConstructor = false )
		{
			
			var info = new CodeRunnerInfo();
			info.Name = name;
			info.IsConstructor = isConstructor;
            info.IsConstructor = false;
            BlockInfoCollector blockInfo = new BlockInfoCollector(root);
            info.blockInfo = blockInfo.Collect(node.Body);

            // Check if the method is static
            foreach (var mod in node.Modifiers)
            {
                if (mod.Text.ToLower().Contains("static"))
                    info.IsStatic = true;
            }

            // Collect all method arguments
            foreach (var param in node.ParameterList.Parameters)
            {
                var methArg = new MethodArgument { Name = param.Identifier.ToString() };
                info.Arguments.Add(methArg);
            }

            // Todo - Add method arguments to info
            return info;
		}
	}
}
