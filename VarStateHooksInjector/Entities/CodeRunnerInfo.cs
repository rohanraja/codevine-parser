using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class CodeRunnerInfo
    {
		public string Name = "";

		public List<MethodArgument> Arguments = new List<MethodArgument>() { };

		public Dictionary<int, List<StatementInfo>> blockInfo = new Dictionary<int, List<StatementInfo>>() { };
       public bool IsConstructor;
    }
}