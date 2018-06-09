using System.Collections.Generic;

namespace VarStateHooksInjector
{
    public class HookTemplates
    {
		// ToDo: Move these to a different static method class
        public static string MethodEnterHook(string filePath, string methodName)
        {
            string expr = string.Format("var mrid = CodeHooks.Instance().OnMethodEnter(@\"{0}\", \"{1}\");\n\n", filePath, methodName);
            return expr;
        }

        public static string LineExecHook(int lineNo, string methodRunState)
        {
			string expr = string.Format("CodeHooks.Instance().LogLineRun(mrid, {0}, CodeHooks.Now(), \"{1}\");\n\n", lineNo, methodRunState);
			return expr;
        }
    }
}