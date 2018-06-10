using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
    public class PropertyWithValueListenerGen
    {
        public PropertyWithValueListenerGen()
        {
        }

		public PropertyDeclarationSyntax GenerateProperty(FieldInfo fieldInfo)
		{
			var identifier = SyntaxFactory.Identifier(fieldInfo.Name);

			SyntaxList<AccessorDeclarationSyntax> accessors = new SyntaxList<AccessorDeclarationSyntax>() { };

			string cvname = FieldGenerator.GetPrefixedName(fieldInfo.Name);

			accessors = accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration, GetAccessorBody(cvname)));
			accessors = accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration, SetAccessorBody(cvname)));

			var accessList = SyntaxFactory.AccessorList(accessors);

			var outProp = SyntaxFactory.PropertyDeclaration(
				new SyntaxList<AttributeListSyntax>() { },
				fieldInfo.Modifiers,
				fieldInfo.Type,
				null,
				identifier,
				accessList
			);
			return outProp;
		}

		BlockSyntax GetAccessorBody(string varName)
		{
            string code = @""" 
            int Prop{
                get{
                return {0};
                }
            }
		""" ;
			code = code.Replace("{0}", varName);

			BlockSyntax outp = GetFirstNodeOfType<BlockSyntax>(code);
			return outp;
		}

		BlockSyntax SetAccessorBody(string varName)
        {
            string code = @""" 
            int Prop{
                set{
                {0} = value;
                }
            }
        """;
			code = code.Replace("{0}", varName);

            BlockSyntax outp = GetFirstNodeOfType<BlockSyntax>(code);
            return outp;
        }

		internal static T GetFirstNodeOfType<T>(string code)
        {
            var root = SyntaxFactory.ParseSyntaxTree(code).GetRoot();
            return GetFirstNodeOfType<T>(root);

        }

		internal static T GetFirstNodeOfType<T>(SyntaxNode root)
        {
            var meth = from methodDeclaration in root.DescendantNodes()
                                                    .OfType<T>()
                       select methodDeclaration;
            return meth.First();
        }
    }
}
