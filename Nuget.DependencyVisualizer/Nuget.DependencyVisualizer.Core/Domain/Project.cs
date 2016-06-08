using System;
using System.Collections.Generic;
using System.Linq;

namespace Nuget.DependencyVisualizer.Core.Domain
{
    public class Project
    {
        public string Name { get; }
        private readonly IList<Package> _installedPackages;

        //public Project(string name)
        //{
        //    if (String.IsNullOrEmpty(name)) throw new ArgumentException("Argument is null or empty", nameof(name));
        //    Name = name;
        //    _installedPackages = new List<Package>();
        //}

        public Project(string name,IEnumerable<Package> installedPackages)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Argument is null or empty", nameof(name));
            Name = name;
            _installedPackages = installedPackages.ToList();
        }

        public void InstallPackage(Package package)
        {
            if (package == null) throw new ArgumentNullException(nameof(package));
            _installedPackages.Add(package);
        }

        public bool HasPackageInstalled(Package package)
        {
            return _installedPackages.Any(p => p.Equals(package));
        }
    }
}