using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;

class CodeRunnerInfoCollector
{
	internal CodeRunnerInfo Collect(MethodDeclarationSyntax node)
	{
		var info = new CodeRunnerInfo();
		info.Name = node.Identifier.ToString();
		info.IsConstructor = false;
		BlockInfoCollector blockInfo = new BlockInfoCollector();
		info.blockInfo = blockInfo.Collect(node.Body);

        // Todo - Add method arguments to info
		return info;
	}

	internal CodeRunnerInfo Collect(ConstructorDeclarationSyntax node)
	{
		var info = new CodeRunnerInfo();
        return info;
	}

	internal CodeRunnerInfo Collect(AccessorDeclarationSyntax node)
	{
		throw new NotImplementedException();
	}
}