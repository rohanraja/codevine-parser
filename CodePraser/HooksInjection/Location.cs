namespace CodePraser.HooksInjection
{
    public class Location
    {
		public Location(int blockId, int statementId)
		{
			BlockId = blockId;
			StatementId = statementId;
		}

		public int BlockId { get; }
		public int StatementId { get; }
	}
}