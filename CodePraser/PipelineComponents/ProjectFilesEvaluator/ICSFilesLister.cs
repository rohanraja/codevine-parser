using System;
using System.Collections.Generic;
using System.Text;

namespace CodePraser
{
    interface ICSFilesLister
    {
        List<string> GetCSCodeFiles(string projPath);
    }
}
