using System;
using System.Globalization;
using CodeRecordHelpers.payloadHolders;

namespace CodeRecordHelpers
{
    public class MethodHelpers
    {
		private static MethodHelpers _instance;

		public Guid CodeRunID = Guid.NewGuid();
		private IMessageDispatcher messageDispatcher;
		private JsonHelper jsonHelper;

		public static MethodHelpers Instance()
		{
			if (_instance == null)
				_instance = new MethodHelpers(new MessageDispatcher());
			
			return _instance;
		}

		public MethodHelpers(IMessageDispatcher dispatcher)
        {
			messageDispatcher = dispatcher;
            jsonHelper = new JsonHelper();
        }

		private void DispatchCodeRunEvent(CodeRunEventHolder codeRunEvent)
        {
            string message = jsonHelper.ToJSON(codeRunEvent);
            string key = "CODE_RUN_EVENTS";

            RedisMessage msg = new RedisMessage(key, message);
            messageDispatcher.DispatchMessage(msg);
        }

		public string GetCurrentTimeStamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture);
        }


		public void LogLineRun(Guid mrid, int lineNo, string timeStamp)
        {
			var codeRunEvent = new CodeRunEventHolder();

			var payload = new LineExecPayloadHolder(mrid, lineNo, timeStamp);

			codeRunEvent.PayLoad = jsonHelper.ToJSON(payload);
			codeRunEvent.EventType = "LINE_EXEC";
			codeRunEvent.CodeRunID = CodeRunID;

			DispatchCodeRunEvent(codeRunEvent);

        }

		public void OnMethodEnter(Guid mrid, string v, string methodName)
        {
        }


	}
}
