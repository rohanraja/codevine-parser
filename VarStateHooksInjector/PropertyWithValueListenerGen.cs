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

		public SyntaxList<PropertyDeclarationSyntax> GenerateProperty(FieldInfo fieldInfo, string clsName = "")
		{
			var outP = new SyntaxList<PropertyDeclarationSyntax>() { };
			foreach(var fieldName in fieldInfo.Names)
			{
				outP = outP.Add(GeneratePropertyForName(fieldInfo, fieldName, clsName));
				
			}
			return outP;

		}

		public PropertyDeclarationSyntax GeneratePropertyForName(FieldInfo fieldInfo, string fieldName, string clsName = "")
		{
			var identifier = SyntaxFactory.Identifier(fieldName);

			SyntaxList<AccessorDeclarationSyntax> accessors = new SyntaxList<AccessorDeclarationSyntax>() { };

			string cvname = FieldGenerator.GetPrefixedName(fieldName);

			string hookexpr = HookTemplates.FieldUpdateHook(fieldName, clsName, "value");

			accessors = accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration, GetAccessorBody(cvname)));
			accessors = accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration, SetAccessorBody(cvname, hookexpr)));

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

		BlockSyntax SetAccessorBody(string varName, string hookexpr)
        {
            string code = @""" 
            int Prop{
                set{
                {1}
                {0} = value;
                }
            }
        """;
			code = code.Replace("{0}", varName);
			code = code.Replace("{1}", hookexpr);

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
