using CodeParserCommon;
namespace CodePraser
{
    public interface IProjectParser
    {
        SourceCodeInfo GetSourceCodeInfo(string rootDir, string proName);
    }
}