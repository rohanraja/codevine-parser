using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using System.Collections.Generic;

namespace CodeParserTests
{
    [TestClass]
    public class MultiListInsertTest
    {

        [TestMethod]
        public void TestInserting()
        {
			var listInserter = new MultiListInserter();

			var lst = new List<string>() { };
			lst.Add("S0");
			lst.Add("S1");
			lst.Add("S2");

			List<KeyValuePair<int, string>> items = new List<KeyValuePair<int, string>>(){};
			items.Add(new KeyValuePair<int, string>(0, "T0"));
			items.Add(new KeyValuePair<int, string>(1, "T1"));
			items.Add(new KeyValuePair<int, string>(2, "T2"));

			listInserter.InsertItems<string>(lst, items);

			Assert.IsTrue(lst[0] == "T0");
			Assert.IsTrue(lst[1] == "S0");
			Assert.IsTrue(lst[2] == "T1");
			Assert.IsTrue(lst[3] == "S1");
			Assert.IsTrue(lst[4] == "T2");
        }
    }
}
