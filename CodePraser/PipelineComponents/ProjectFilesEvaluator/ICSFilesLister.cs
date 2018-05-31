using System;
using System.Collections.Generic;
using System.Text;

namespace CodePraser
{
    public interface ICSFilesLister
    {
        List<string> GetCSCodeFiles(string projPath);
    }
}
