using System;

namespace Nuget.DependencyVisualizer.Core.Domain
{
    public class Package
    {
        public string Name { get; }
        public Package(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var package = obj as Package;
            if (package == null)
                return false;
            return package.Name.Equals(Name,StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}