﻿using System;
namespace HooksInjector
{
    public class Statement
    {
		public Statement(Location location, int lineNo)
        {
			Location = location;
			LineNo = lineNo;
		}

		public Location Location { get; }
		public int LineNo { get; }
	}
}
