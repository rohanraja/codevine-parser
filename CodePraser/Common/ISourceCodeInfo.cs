using System.Collections.Generic;

namespace CodePraser
{
    public interface ISourceCodeInfo
    {
		List<ISourceFile> ListCodeFiles();
		ISourceFile GetProjectFile();
    }
}