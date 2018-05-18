using System;
using CodeRecordHelpers;

namespace TestCSharpProject
{
    public class ClassB
    {
		public string name = "";


        public ClassB(string pName)
        {
			name = pName;
        }


		public void MethodB_1()
		{
			name = name + " MODIFIED";

		}

    }
}
