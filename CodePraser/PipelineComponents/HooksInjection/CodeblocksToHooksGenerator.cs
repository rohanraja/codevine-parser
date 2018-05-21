using System;
using System.Collections.Generic;
using CodePraser.HooksInjection;

namespace CodePraser.PipelineComponents.HooksInjection
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
			
			outP.Add(MethodEnterHook(block.sourceFile.FilePath, block.methodName));

            foreach (var statement in block.Statements)
            {
				var kvp = LineExecHook(statement.Location.StatementId, statement.LineNo);
                outP.Add(kvp);

            }
            return outP;
        }

		// ToDo: Move these to a different static method class
		private KeyValuePair<int, string> MethodEnterHook(string filePath, string methodName)
        {
			string expr = string.Format("var mrid = CodeHooks.Instance().OnMethodEnter(\"{0}\", \"{1}\");\n\n", filePath, methodName);
            return new KeyValuePair<int, string>(0, expr);
        }

        private KeyValuePair<int, string> LineExecHook(int statementId, int lineNo)
        {
            string expr = string.Format("CodeHooks.Instance().LogLineRun(mrid, {0}, CodeHooks.Now());\n\n", lineNo);
			return new KeyValuePair<int, string>(statementId, expr);
        }
	}
}
