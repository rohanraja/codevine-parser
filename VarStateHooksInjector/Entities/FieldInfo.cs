using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VarStateHooksInjector.Entities
{
	public class FieldInfo
	{
		public List<string> Names = new List<string>(){};

		public SyntaxTokenList Modifiers ;
		public TypeSyntax Type;
	}
}