using System;
using System.Collections.Generic;

namespace CodePraser
{
    public class MultiListInserter
    {
        public MultiListInserter()
        {
        }

		public void InsertItems<T> ( List<T> list, List< KeyValuePair<int, T> > items )
		{
			int insertCount = 0;
			List<T> outList = list;

			foreach(var item in items)
			{
				outList.Insert(item.Key + insertCount, item.Value);
				insertCount++;
			}
		}
    }
}
