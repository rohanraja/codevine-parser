using CodePraser.HooksInjection;

namespace CodePraser
{
    public interface ISourceFileHooker
    {
		void AddHooksToSourceCode(SourceCodeInfo sourceCode);
    }
}