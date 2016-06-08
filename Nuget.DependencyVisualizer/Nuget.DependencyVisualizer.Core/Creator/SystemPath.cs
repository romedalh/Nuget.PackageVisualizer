using System.IO;

namespace Nuget.DependencyVisualizer.Core.Creator
{
    public class SystemPath
    {
        public string Path { get; private set; }

        public SystemPath(string path)
        {
            Directory.Exists(path);
            Path = path;
        }
    }
}