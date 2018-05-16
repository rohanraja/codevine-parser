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
            //string message = jsonHelper.ToJSON(codeRunEvent);
            string key = "CODE_RUN_EVENTS";
			key = string.Format("resque:queue:{0}", key);

			ResqueMessage remsg = new ResqueMessage();
			remsg.Class = "CodeRunEventProcessor";
			remsg.args.Add(codeRunEvent.CodeRunID.ToString());
			remsg.args.Add(codeRunEvent.EventType);
			remsg.args.Add(codeRunEvent.PayLoad);

			string message = jsonHelper.ToJSON(remsg);

            RedisMessage msg = new RedisMessage(key, message);
            messageDispatcher.DispatchMessage(msg);
        }

		public static string GetCurrentTimeStamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture);
        }


		public void LogLineRun(Guid mrid, int lineNo, string timeStamp)
		{

			var payload = new LineExecPayloadHolder(mrid, lineNo, timeStamp);
			var eventType = "LINE_EXEC";

			DispatchCodeRunEvent(payload, eventType);
            
		}

		private void DispatchCodeRunEvent(object payload, string eventType)
		{
			var codeRunEvent = new CodeRunEventHolder();
			codeRunEvent.PayLoad = jsonHelper.ToJSON(payload);
			codeRunEvent.EventType = eventType;
			codeRunEvent.CodeRunID = CodeRunID;

			DispatchCodeRunEvent(codeRunEvent);
		}

		public void OnMethodEnter(Guid mrid, string v, string methodName)
        {
        }


	}
}
