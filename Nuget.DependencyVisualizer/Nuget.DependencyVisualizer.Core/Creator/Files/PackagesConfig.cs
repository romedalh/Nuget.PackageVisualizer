using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Nuget.DependencyVisualizer.Core.Domain;
using Nuget.DependencyVisualizer.Core.Exceptions;

namespace Nuget.DependencyVisualizer.Core.Creator.Files
{
    public class PackagesConfig
    {
        private readonly FileInfo _file;

        public PackagesConfig(FileInfo file)
        {
            _file = file;
        }

        public IEnumerable<Package> ExtractPackages()
        {
            if (!FielExists(_file))
            {
                return new List<Package>();
            }
            var reader = XmlReader.Create(_file.FullName);
            XDocument doc = null;
            try
            {
                doc = XDocument.Load(_file.FullName);
            }
            catch (XmlException)
            {
                throw new IncorrectFileException($"incorrect xml file.[Filename:{_file.Name}]");
            }
            return doc.Descendants("package").Select(packageNode => new Package(packageNode.Attribute("id").Value));
        }

        private bool FielExists(FileInfo file)
        {
            return _file != null && file.Exists;
        }
    }
}