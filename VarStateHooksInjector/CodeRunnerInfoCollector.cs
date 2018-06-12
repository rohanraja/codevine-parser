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
    		var info = new CodeRunnerInfo();
    		info.Name = node.Identifier.ToString();
    		info.IsConstructor = false;
    		BlockInfoCollector blockInfo = new BlockInfoCollector(root);
    		info.blockInfo = blockInfo.Collect(node.Body);
            
			foreach(var mod in node.Modifiers)
            {
                if (mod.Text.ToLower().Contains("static"))
                    info.IsStatic = true;
            }

            // Todo - Add method arguments to info
    		return info;
    	}

    	internal CodeRunnerInfo Collect(ConstructorDeclarationSyntax node)
    	{
            // Todo - Remove this duplication
    		var info = new CodeRunnerInfo();
    		info.Name = node.Identifier.ToString();
            info.IsConstructor = true;
            BlockInfoCollector blockInfo = new BlockInfoCollector(root);
            info.blockInfo = blockInfo.Collect(node.Body);
            return info;
    	}

    	internal CodeRunnerInfo Collect(AccessorDeclarationSyntax node)
    	{
        // Todo - implement this
        return new CodeRunnerInfo();
    	}
	}
}
