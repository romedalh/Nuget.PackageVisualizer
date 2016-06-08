using System.IO;
using Nuget.DependencyVisualizer.Core.Creator.Files;

namespace Nuget.DependencyVisualizer.Core.Creator.Model
{
    public class ProjectFiles
    {
        public DirectoryInfo Directory { get; set; }
        public FileInfo CsprojFile { get; set; }
        public PackagesConfig PackagesFile { get; set; }
    }
}