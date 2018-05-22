using System;
using System.Collections.Generic;
using CodePraser;
using CodePraser.HooksInjection;
using CodePraser.PipelineComponents.HooksInjection;
using CodeParserCommon;

namespace CodeParserTests
{
    public class PipelineComponentsFactory
    {
		public static List<CodeBlock> GenerateTestCodeBlock(SourceFile sourceFile)
        {
            List<CodeBlock> blocks = new List<CodeBlock>() { };
            CodeBlock codeBlock = new CodeBlock(sourceFile, "MethodA_1", new Location(1, 0), true);
            codeBlock.AddStatement(new Statement(new Location(1, 0), 19));
            blocks.Add(null);
            blocks.Add(codeBlock);
            return blocks;
        }

		public static List<Hooks> GenerateHookList(SourceFile sourceFile)
		{
			List<KeyValuePair<int, string>> pairs = new List<KeyValuePair<int, string>>(){};
			pairs.Add(new KeyValuePair<int, string>(0, "var mrid = CodeHooks.Instance().OnMethodEnter(\\\"ClassA.cs\", \"MethodA_1\");\n\n"));
			pairs.Add(new KeyValuePair<int, string>(0, "CodeHooks.Instance().LogLineRun(mrid, 19, CodeHooks.Now());\n\n" ));
			Hooks hooks = new Hooks(pairs);
			Hooks hook2 = new Hooks();
			var hooksList = new List<Hooks>() { };
			hooksList.Add(hook2);
			hooksList.Add(hooks);
			return hooksList;
		}
	}
}
