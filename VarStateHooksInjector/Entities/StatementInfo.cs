using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
	public class StatementInfo
	{
		public int LineNo;

		public bool IsLocalVarDeclaration;

		public bool IsLovaVarStateChanger;

		public List<string> LocalVarNames = new List<string>() { };

	}
}