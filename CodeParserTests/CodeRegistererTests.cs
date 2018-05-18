using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodeRecordHelpers;
using Moq;

namespace CodeParserTests
{
    [TestClass]
    public class CodeRegistererTests
    {

        [TestMethod]
        public void TestRegesteringProject()
        {
			SourceCodeInfo sourceCode = new SourceCodeInfo(GitTests.RepoPath);
			sourceCode.AddCodeFile("ClassA.cs");
			sourceCode.AddCodeFile("ClassB.cs");
			sourceCode.AddCodeFile("Program.cs");

			var mock = new Mock<IMessageDispatcher>();
            RedisMessage msg = new RedisMessage("", "");
            mock.Setup(x => x.DispatchMessage(It.IsAny<RedisMessage>())).Callback((RedisMessage pmsg) => msg = pmsg);
            var codeHooks = new CodeHooks(mock.Object);

			CodeRegisterer codeRegisterer = new CodeRegisterer(codeHooks);

			codeRegisterer.Register(sourceCode);


			mock.Verify(x => x.DispatchMessage(It.IsAny<RedisMessage>()), Times.AtLeast(3));

            Assert.IsTrue(msg.GetKey().Contains("CODE_RUN_EVENTS"));
			Assert.IsTrue(msg.GetMessage().Contains("ADD_SOURCE_FILE"));
        }

    }
}
