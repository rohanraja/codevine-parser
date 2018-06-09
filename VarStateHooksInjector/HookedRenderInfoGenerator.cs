using System;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class HookedRenderInfoGenerator
	{
		public CodeRunBlockRenderingInfo CodeRunBlockRenderInfoForMethod(ClassInfo classInfo, int id)
		{
			return null;
		}

		public CodeRunBlockRenderingInfo CodeRunBlockRenderInfoForConstructor(ClassInfo classInfo, int id)
		{
			throw new NotImplementedException();
		}
	}
}