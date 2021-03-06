﻿using System;
using System.Collections.Generic;
using CodeParserCommon;

namespace HooksInjector
{
    public class CodeBlock
    {
		public readonly SourceFile sourceFile;
		public readonly string methodName;
		private readonly Location location;
		public bool IsMethod { get; }

		public List<Statement> Statements = new List<Statement>() { };

		public CodeBlock(SourceFile sourceFile, string methodName, Location location, bool isMethod)
        {
			this.sourceFile = sourceFile;
			this.methodName = methodName;
			this.location = location;
			IsMethod = isMethod;
		}

		public void AddStatement(Statement s)
		{
			Statements.Add(s);
		}

	}
}
