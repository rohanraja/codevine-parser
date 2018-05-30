using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePraser;
using CodeRecordHelpers;
using Moq;
using CodeParserCommon;

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

			var mock = new Mock<IMessageDispatcher>();
            RedisMessage msg = new RedisMessage("", "");
            mock.Setup(x => x.DispatchMessage(It.IsAny<RedisMessage>())).Callback((RedisMessage pmsg) => msg = pmsg);
            var codeHooks = new CodeHooks(mock.Object);

			CodeRegisterer codeRegisterer = new CodeRegisterer(codeHooks);

			codeRegisterer.SendCodeContentsToServer(sourceCode);


			mock.Verify(x => x.DispatchMessage(It.IsAny<RedisMessage>()), Times.AtLeast(1));

            Assert.IsTrue(msg.GetKey().Contains("CODE_RUN_EVENTS"));
			Assert.IsTrue(msg.GetMessage().Contains("ADD_SOURCE_FILE"));
			Assert.IsTrue(msg.GetMessage().Contains("ClassA.cs"));
			Assert.IsTrue(msg.GetMessage().Contains("MethodA_1"));
        }

		//[TestMethod]
        public void RegisterInRails()
		{
			SourceCodeInfo sourceCode = new SourceCodeInfo(GitTests.RepoPath);
            sourceCode.AddCodeFile("ClassA.cs");
			sourceCode.AddCodeFile("ClassB.cs");
			sourceCode.AddCodeFile("Program.cs");

			CodeRegisterer codeRegisterer = new CodeRegisterer();
            codeRegisterer.SendCodeContentsToServer(sourceCode);
		}

    }
}
