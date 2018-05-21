using System;
using System.Collections.Generic;

namespace CodePraser.HooksInjection
{
    public class CodeBlock
    {
		public readonly SourceFile sourceFile;
		public readonly string methodName;
		private readonly Location location;
		public bool IsMethod { get; }

		public List<Statement> Statements = new List<Statement>() { };

		public CodeBlock(SourceFile sourceFile, string methodName, Location location, bool isMethod)
        {
			this.sourceFile = sourceFile;
			this.methodName = methodName;
			this.location = location;
			IsMethod = isMethod;
		}

		public void AddStatement(Statement s)
		{
			Statements.Add(s);
		}

        internal List<KeyValuePair<int, string>> GetHooks()
        {
			List<KeyValuePair<int, string>> outP = new List<KeyValuePair<int, string>>(){};
			outP.Add(MethodEnterHook());

			foreach(var statement in Statements)
			{
				var kvp = LineExecHook(statement);
				outP.Add(kvp);
				
			}
			return outP;
        }

		private KeyValuePair<int, string> MethodEnterHook()
		{
			string expr = string.Format("var mrid = CodeHooks.Instance().OnMethodEnter(\"{0}\", \"{1}\");\n\n", sourceFile.FilePath ,methodName);
			return new KeyValuePair<int, string>(0, expr) ;
		}

		private KeyValuePair<int, string> LineExecHook(Statement statement)
        {
			string expr = string.Format("CodeHooks.Instance().LogLineRun(mrid, {0}, CodeHooks.Now());\n\n", statement.LineNo);
			return new KeyValuePair<int, string>(statement.Location.StatementId, expr);
        }
	}
}
