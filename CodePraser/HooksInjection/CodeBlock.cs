using System;
namespace CodePraser.HooksInjection
{
    public class CodeBlock
    {
		private readonly SourceFile sourceFile;
		public readonly string methodName;
		private readonly Location location;
		public bool IsMethod { get; }

		public CodeBlock(SourceFile sourceFile, string methodName, Location location, bool isMethod)
        {
			this.sourceFile = sourceFile;
			this.methodName = methodName;
			this.location = location;
			IsMethod = isMethod;
		}

	}
}
