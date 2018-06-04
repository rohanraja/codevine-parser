using System;
using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class CodeRunBlockRenderingInfo
    {
		private Dictionary<int, List<string>> renderingInfo;

		public CodeRunBlockRenderingInfo()
        {
        }

		public CodeRunBlockRenderingInfo(Dictionary<int, List<string>> renderingInfo)
		{
			this.renderingInfo = renderingInfo;
		}

		internal bool ContainsId(int bid)
		{
			return renderingInfo.ContainsKey(bid);
		}

		internal List<string> GetStatementsForId(int bid)
		{
			return renderingInfo[bid];
		}
	}
}
