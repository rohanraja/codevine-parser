namespace CodePraser
{
    public interface ICodeRegisterer
    {
        void SendCodeContentsToServer(SourceCodeInfo sourceCode);
    }
}