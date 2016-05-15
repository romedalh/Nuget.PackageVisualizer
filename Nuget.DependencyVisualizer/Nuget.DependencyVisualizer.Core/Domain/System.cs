using System.Collections.Generic;
using System.Linq;

namespace Nuget.DependencyVisualizer.Core.Domain
{
    public class System
    {
        private readonly IList<Project> _associatedProjects;

        public System(IEnumerable<Project> associatedProjects)
        {
            _associatedProjects = associatedProjects.ToList();
        }

        public IEnumerable<Project> FindProjectsWithInstalledPackage(Package package)
        {
            return _associatedProjects.Where(project => project.HasPackageInstalled(package));
        }
    }
}