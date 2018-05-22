namespace CodeParserCommon
{
    public interface ISourceFileHooker
    {
		void AddHooksToSourceCode(SourceCodeInfo sourceCode);
    }
}