using System;

namespace CodeRecordHelpers
{
    public class MethodHelpers
    {
		private static MethodHelpers _instance;

		public static MethodHelpers Instance()
		{
			if (_instance == null)
				_instance = new MethodHelpers();
			
			return _instance;
		}

		public void OnMethodEnter(Guid mrid, string v, string methodName)
		{
		}
	}
}
