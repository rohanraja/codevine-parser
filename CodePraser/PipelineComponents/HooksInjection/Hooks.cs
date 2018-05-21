using System;
using System.Collections.Generic;

namespace CodePraser.PipelineComponents.HooksInjection
{
    public class Hooks
    {
		public readonly List<KeyValuePair<int, string>> Pairs = new List<KeyValuePair<int, string>>(){};

		public Hooks(List<KeyValuePair<int, string>> pairs)
        {
			this.Pairs = pairs;
		}
		public Hooks()
		{
			
		}
    }
}
