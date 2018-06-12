using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class CodeRunnerInfo
    {
		public string Name = "";

		public List<MethodArgument> Arguments = new List<MethodArgument>() { };

		public Dictionary<int, BlockInfo> blockInfo = new Dictionary<int, BlockInfo>() { };
       public bool IsConstructor;
		public bool IsStatic = false;
    }
}