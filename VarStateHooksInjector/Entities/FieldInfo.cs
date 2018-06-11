using System;
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
		public bool IsStatic = false;
		internal bool IsAbstract = false;

        public bool ShouldBeHooked()
		{
			return (!IsStatic) && (!IsAbstract);
		}

		internal static bool ShouldBeHooked(FieldDeclarationSyntax node)
		{
			bool IsStatic = false, IsAbstract = false;

			foreach (var mod in node.Modifiers)
            {
                if (mod.Text.ToLower().Contains("static"))
                    IsStatic = true;

                if (mod.Text.ToLower().Contains("abstract"))
                    IsAbstract = true;
            }
			return (!IsStatic) && (!IsAbstract);
		}
	}
}