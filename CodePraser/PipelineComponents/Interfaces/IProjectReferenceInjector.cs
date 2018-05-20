namespace CodePraser
{
    public interface IProjectReferenceInjector
    {
        void InjectReference(SourceCodeInfo sourceCodeInfo, string projectFileName);
    }
}