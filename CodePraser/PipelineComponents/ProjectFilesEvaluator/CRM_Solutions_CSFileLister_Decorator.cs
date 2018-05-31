using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Xml;

namespace CodePraser
{
    public class CRM_Solutions_CSFileLister_Decorator : ICSFilesLister
    {
        public List<string> GetCSCodeFiles(string projPath)
        {
            var origContents = File.ReadAllText(projPath);

            string modded = TryRemoveImportStatements(origContents);

            File.WriteAllText(projPath, modded);

            var outP =  new BuildAlyzerLister().GetCSCodeFiles(projPath);

            File.WriteAllText(projPath, origContents);

            return outP;
        }

        private string TryRemoveImportStatements(string origContents)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(origContents);
            var imports = xml.GetElementsByTagName("Import");
            for(int i=0; i< imports.Count; i++)
            {
                var item = imports.Item(i);
                if (item.OuterXml.Contains("PluginAssembly.target"))
                    item.ParentNode.RemoveChild(item);
            }
            return xml.OuterXml;
        }
    }
}
