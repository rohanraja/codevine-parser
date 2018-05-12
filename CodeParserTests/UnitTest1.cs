using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;

namespace CodeParserTests
{
    [TestClass]
    public class UnitTest1
    {

		private TestingDataReader testDataReader;

		[TestInitialize]
        public void setUp()
		{
			testDataReader = TestingDataReader.GetReader();
			Assert.IsTrue(testDataReader.GetMainFile().Contains("void Main"));
		}


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
