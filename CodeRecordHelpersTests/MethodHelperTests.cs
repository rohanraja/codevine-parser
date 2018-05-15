using CodeRecordHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeRecordHelpersTests
{
    [TestClass]
    public class MethodHelperTests
    {
		MethodHelpers methodHelpers;

		[TestInitialize]
        public void Init()
		{
		}

		[TestCleanup]
        public void Clean()
        {
        }

        [TestMethod]
        public void TestLoggingLine()
        {
			var mock = new Mock<IMessageDispatcher>();

			RedisMessage msg = new RedisMessage("","");

			mock.Setup(x => x.DispatchMessage(It.IsAny<RedisMessage>())).Callback( (RedisMessage pmsg) => msg = pmsg ) ;
			methodHelpers = new MethodHelpers( mock.Object );

			methodHelpers.LogLineRun(System.Guid.NewGuid(), 95, "testDate");

			mock.Verify(x => x.DispatchMessage(It.IsAny<RedisMessage>()), Times.Once());

			Assert.AreEqual(msg.GetKey(), "CODE_RUN_EVENTS");
			Assert.IsTrue(msg.GetMessage().Contains("95"));
			Assert.IsTrue(msg.GetMessage().Contains("testDate"));

        }
    }
}