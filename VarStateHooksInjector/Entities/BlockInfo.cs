using System;
using System.Collections.Generic;

namespace VarStateHooksInjector.Entities
{
    public class BlockInfo
    {
		public List<StatementInfo> StatementInfos = new List<StatementInfo>() { };
		public int CloseBraceLineNo = -1;

        public BlockInfo()
        {
        }

		public BlockInfo(List<StatementInfo> statementInfos)
		{
			StatementInfos = statementInfos;
		}

		public int Count { 

			get{
				return StatementInfos.Count;
			} 
		}

		public void Add(StatementInfo sinfo)
		{
			StatementInfos.Add(sinfo);
		}
    }
}
