using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodeReader;

namespace CodeParserTests
{

	[TestClass]
    public class UnitTest1
    {

		private DataReader testDataReader;

		[TestInitialize]
        public void setUp()
		{
			testDataReader = TestDummyDataReader.GetTestReader();
			Assert.IsTrue(testDataReader.GetMainFile().Contains("void Main"));
		}


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
