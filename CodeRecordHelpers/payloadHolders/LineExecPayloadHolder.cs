using System;
namespace CodeRecordHelpers.payloadHolders
{
    public class LineExecPayloadHolder
    {
		public Guid Mrid;
		public int LineNo;
		public string TimeStamp;

		public LineExecPayloadHolder(Guid mrid, int lineNo, string timeStamp)
		{
			this.Mrid = mrid;
			this.LineNo = lineNo;
			this.TimeStamp = timeStamp;
		}
	}
}
