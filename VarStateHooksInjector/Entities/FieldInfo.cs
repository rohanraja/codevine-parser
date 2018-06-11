using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VarStateHooksInjector.Entities
{
	public class FieldInfo
	{
		public string Name;

		public SyntaxTokenList Modifiers ;
		public TypeSyntax Type;
	}
}