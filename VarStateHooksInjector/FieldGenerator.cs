using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	internal class FieldGenerator
	{
		private ClassInfo classInfo;

		public FieldGenerator(ClassInfo classInfo)
		{
			this.classInfo = classInfo;
		}

		internal FieldDeclarationSyntax Generate(FieldDeclarationSyntax node, int id)
		{
			throw new NotImplementedException();
		}
	}
}