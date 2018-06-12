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
		public static string FieldUpdateHook(string varName, string className, string cvVarName)
        {
			string expr = string.Format("CodeHooks.Instance().SendFieldUpdate(this.GetHashCode(), \"{0}\", \"raw\", \"{1}\", {2}, CodeHooks.Now());\n\n", varName, className, cvVarName);
			return expr;
        }

		public static string LocalVarUpdateHook(string varName, string className="")
        {
			string expr = string.Format("CodeHooks.Instance().LocalVarUpdate(mrid, \"{0}\", \"{1}\", {2}, CodeHooks.Now());\n\n", varName, className, varName);
            return expr;
        }
    }
}