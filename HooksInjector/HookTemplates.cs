using System.Collections.Generic;

namespace HooksInjector
{
    public class HookTemplates
    {
		// ToDo: Move these to a different static method class
        public static KeyValuePair<int, string> MethodEnterHook(string filePath, string methodName)
        {
            string expr = string.Format("var mrid = CodeHooks.Instance().OnMethodEnter(@\"{0}\", \"{1}\");\n\n", filePath, methodName);
			expr = methodName == "" ? "" : expr;
            return new KeyValuePair<int, string>(0, expr);
        }

        public static KeyValuePair<int, string> LineExecHook(int statementId, int lineNo, string methodRunState)
        {
			string expr = string.Format("CodeHooks.Instance().LogLineRun(mrid, {0}, CodeHooks.Now(), \"{1}\");\n\n", lineNo, methodRunState);
            return new KeyValuePair<int, string>(statementId, expr);
        }
    }
}