using System;
using System.Collections.Generic;
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

		private void DispatchCodeRunEvent_ToResque(List<string> args)
        {
            //string message = jsonHelper.ToJSON(codeRunEvent);
            string key = "CODE_RUN_EVENTS";
			key = string.Format("resque:queue:{0}", key);

			ResqueMessage remsg = new ResqueMessage();
			remsg.Class = "CodeRunEventProcessor";
			remsg.args = args;

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
			string Payload = jsonHelper.ToJSON(payload);

			var strArgs = new List<string> () { };
			strArgs.Add(CodeRunID.ToString());
            strArgs.Add(eventType);
			strArgs.Add(Payload);

			string key = "CODE_RUN_EVENTS";
            key = string.Format("resque:queue:{0}", key);
			string railsClassName = "CodeRunEventProcessor";

			EnqueueResqueMessage(key, railsClassName, strArgs);
		}

		private void EnqueueResqueMessage(string key, string railsClassName, List<string> strArgs)
		{

            ResqueMessage remsg = new ResqueMessage();
			remsg.Class = railsClassName;
			remsg.args = strArgs;
            string message = jsonHelper.ToJSON(remsg);

            RedisMessage msg = new RedisMessage(key, message);
            messageDispatcher.DispatchMessage(msg);
		}

		public void OnMethodEnter(Guid mrid, string v, string methodName)
        {
        }


	}
}
