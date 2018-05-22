using System;
using System.Collections.Generic;
using CodeParserCommon;

namespace HooksInjector
{
    public class CodeblocksToHooksGenerator
    {
        public CodeblocksToHooksGenerator()
        {
        }

		public List<Hooks> GenerateHooks(List<CodeBlock> blocks)
		{
			List<Hooks> outP = new List<Hooks>() { };

			foreach(var block in blocks)
			{
				var pair = GetHookPairs(block);
				var hooks = new Hooks(pair);
				outP.Add(hooks);
			}

			return outP;
		}

		internal List<KeyValuePair<int, string>> GetHookPairs(CodeBlock block)
        {
            List<KeyValuePair<int, string>> outP = new List<KeyValuePair<int, string>>() { };

			if (block == null)
				return outP;
			
			outP.Add(HookTemplates.MethodEnterHook(block.sourceFile.FilePath, block.methodName));

            foreach (var statement in block.Statements)
            {
				var kvp = HookTemplates.LineExecHook(statement.Location.StatementId, statement.LineNo, statement.MethodRunState);
                outP.Add(kvp);

            }
            return outP;
        }

	}
}
