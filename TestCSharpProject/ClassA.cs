using System;
using System.Collections.Generic;
using CodeRecordHelpers;
using Newtonsoft.Json;


namespace TestCSharpProject
{
    public class ClassA
    {
		public ClassB clsB; 
		public int count = 0;

		public ClassA()
        {
			clsB = new ClassB("teststr");
        }


		public void MethodA_1()
        {
			clsB.MethodB_1();
			count = 22;
        }

        public int GetCount()
		{
			return count;
		}


    }
}
