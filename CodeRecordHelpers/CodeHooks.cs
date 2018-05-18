using System;
using System.Collections.Generic;
using System.Globalization;
using CodeRecordHelpers.payloadHolders;

namespace CodeRecordHelpers
{

	public class CodeHooks
    {
		private static CodeHooks _instance;

		public Guid CodeRunID = Guid.NewGuid();
		private HookHelpers hookHelpers;

		public static string Now()
		{
			return System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
		}

		public static CodeHooks Instance()
		{
			if (_instance == null)
				_instance = new CodeHooks(new MessageDispatcher());
			
			return _instance;
		}

		public CodeHooks(IMessageDispatcher dispatcher)
        {
			hookHelpers = new HookHelpers(dispatcher);
        }



		public void AddSourceFile(string relativeFilePath, string code)
        {
            var eventType = "ADD_SOURCE_FILE";


            var payload = new List<string>() { };
			payload.Add(relativeFilePath);
			payload.Add(code);

			hookHelpers.DispatchCodeRunEvent(CodeRunID.ToString(), payload, eventType);
        }

		public Guid OnMethodEnter(string relativeFilePath, string methodName)
        {
            var eventType = "METHOD_ENTER";


			var mrid = Guid.NewGuid();
            var payload = new List<string>() { };
            payload.Add(mrid.ToString());
            payload.Add(relativeFilePath);
            payload.Add(methodName);

            hookHelpers.DispatchCodeRunEvent(CodeRunID.ToString(), payload, eventType);

			return mrid;
        }

		public void LogLineRun(Guid mrid, int lineNo, string timeStamp)
        {

            //var payload = new LineExecPayloadHolder(mrid, lineNo, timeStamp);
            var eventType = "LINE_EXEC";

			var payload = new List<string>() { };
			payload.Add(mrid.ToString());
			payload.Add(lineNo.ToString());
			payload.Add(timeStamp);


			hookHelpers.DispatchCodeRunEvent(CodeRunID.ToString(), payload, eventType);

        }

	}
}
