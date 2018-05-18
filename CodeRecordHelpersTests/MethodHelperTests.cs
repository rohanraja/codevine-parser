using CodeRecordHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeRecordHelpersTests
{
    [TestClass]
    public class MethodHelperTests
    {
		CodeHooks methodHelpers;

		[TestInitialize]
        public void Init()
		{
		}

		[TestCleanup]
        public void Clean()
        {
        }

        [TestMethod]
        public void TestLoggingLine_MockDispatcher()
        {
			var mock = new Mock<IMessageDispatcher>();

			RedisMessage msg = new RedisMessage("","");

			mock.Setup(x => x.DispatchMessage(It.IsAny<RedisMessage>())).Callback( (RedisMessage pmsg) => msg = pmsg ) ;

			methodHelpers = new CodeHooks( mock.Object );

			methodHelpers.LogLineRun(System.Guid.NewGuid(), 95, "testDate");

			mock.Verify(x => x.DispatchMessage(It.IsAny<RedisMessage>()), Times.Once());

			Assert.IsTrue(msg.GetKey().Contains("CODE_RUN_EVENTS"));
			Assert.IsTrue(msg.GetMessage().Contains("95"));
			Assert.IsTrue(msg.GetMessage().Contains("testDate"));
			Assert.IsTrue(msg.GetMessage().Contains("LINE_EXEC"));

        }

		[TestMethod]
        public void TestLoggingLine_REDIS()
		{
			methodHelpers = CodeHooks.Instance();
			System.Guid mrid = System.Guid.NewGuid();
			methodHelpers.AddSourceFile("newClass.cs", "line1\nline2\nline3\nline4\nline5");
			methodHelpers.OnMethodEnter(mrid, "newClass.cs", "MethodA");
			methodHelpers.LogLineRun(mrid, 0, "09-04-1993");
			methodHelpers.LogLineRun(mrid, 1, "09-04-1994");
			methodHelpers.LogLineRun(mrid, 3, "09-04-1995");
		}
    }
}
