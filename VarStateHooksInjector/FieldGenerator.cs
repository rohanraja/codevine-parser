using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class FieldGenerator
	{
		private ClassInfo classInfo;
		public static string VAR_PREFIX="CodeVine_";

		public static string GetPrefixedName(string originalName)
		{
			return string.Format("{0}{1}", VAR_PREFIX, originalName);
		}

		public FieldGenerator(ClassInfo classInfo)
		{
			this.classInfo = classInfo;
		}
        
		public FieldDeclarationSyntax Generate(FieldDeclarationSyntax node, int id)
		{
            // Prevent hooking static and abstract fields
			if (!FieldInfo.ShouldBeHooked(node))
				return node;
			
			var VarDecs = node.Declaration.Variables;
			Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax> newVars = default(Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax>);

			foreach(var Vardec in VarDecs)
			{
				string newName = GetPrefixedName(Vardec.Identifier.ToString());
				var newId = SyntaxFactory.Identifier(newName);
				newVars = newVars.Add(Vardec.WithIdentifier(newId));
			}
			return node.WithDeclaration(node.Declaration.WithVariables(newVars));
		}
	}
}