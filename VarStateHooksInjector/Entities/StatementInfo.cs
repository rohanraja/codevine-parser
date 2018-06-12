using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
	public class StatementInfo
	{
		public int LineNo;

		public bool IsLocalVarDeclaration = false;

		public bool IsLocalVarStateChanger = false;

		public List<string> LocalVarNames = new List<string>() { };

	}
}