using System;
using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class CodeRunBlockRenderingInfo
    {
		public Dictionary<int, List<string>> renderingInfo = new Dictionary<int, List<string>>() { };

		public CodeRunBlockRenderingInfo()
        {
        }

		public CodeRunBlockRenderingInfo(Dictionary<int, List<string>> renderingInfo)
		{
			this.renderingInfo = renderingInfo;
		}

		public bool ContainsId(int bid)
		{
			return renderingInfo.ContainsKey(bid);
		}

		public List<string> GetStatementsForId(int bid)
		{
			return renderingInfo[bid];
		}
	}
}
