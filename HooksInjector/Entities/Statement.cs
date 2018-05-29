using System;
namespace HooksInjector
{
    public class Statement
    {
		public Statement(Location location, int lineNo, string methodRunState = "RUNNING")
        {
			Location = location;
			LineNo = lineNo;
			MethodRunState = methodRunState;
		}

		public Location Location { get; }
		public int LineNo { get; }
		public string MethodRunState { get; }
	}
}
