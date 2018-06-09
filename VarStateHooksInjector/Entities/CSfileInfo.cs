using System;
using System.Collections.Generic;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class CSfileInfo
	{
		public List<ClassInfo> Classes = new List<ClassInfo>() { };

		internal ClassInfo GetClassInfo(int id)
		{
			return Classes[id];
		}
	}
}