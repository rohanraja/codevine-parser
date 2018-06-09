using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace VarStateHooksInjector
{
    public class HookInjectionPipeline
    {
        public HookInjectionPipeline()
        {
        }

		public string AddHooksToSourceFile(string fileName, string fileContents)
        {
            
			SyntaxTree root = GetRoot(fileContents);
			var collector = new CSFileInfoCollector(root, fileName);
			CSfileInfo cSfileInfo = collector.Collect(root.GetRoot());

			CSFileInfoWriter writer = new CSFileInfoWriter(cSfileInfo);
            var newRoot = writer.Visit(root.GetRoot());

			return newRoot.GetText().ToString();
        }

		private SyntaxTree GetRoot(string code)
		{
			var root = SyntaxFactory.ParseSyntaxTree(code);
            return root;
		}
	}
}
