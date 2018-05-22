using CodePraser.HooksInjection;
using CodeParserCommon;

namespace CodePraser
{
    public interface ISourceFileHooker
    {
		void AddHooksToSourceCode(SourceCodeInfo sourceCode);
    }
}