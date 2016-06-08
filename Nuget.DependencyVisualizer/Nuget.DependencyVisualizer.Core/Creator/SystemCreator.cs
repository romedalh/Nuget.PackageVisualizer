using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuget.DependencyVisualizer.Core.Creator.Files;
using Nuget.DependencyVisualizer.Core.Creator.Model;
using Nuget.DependencyVisualizer.Core.Domain;

namespace Nuget.DependencyVisualizer.Core.Creator
{
    public class SystemCreator
    {
        public Domain.System CreateFromPath(SystemPath path)
        {
            var projects = GetProjects(path);
            return new Domain.System(projects);
        }

        private IEnumerable<Project> GetProjects(SystemPath path)
        {
            var projects = GetProjectFiles(path);
            foreach (var projectFiles in projects)
            {
                var packages = CreatePackages(projectFiles.PackagesFile);
                yield return new Project(projectFiles.CsprojFile.Name, packages);
            }
        }

        private static IEnumerable<ProjectFiles> GetProjectFiles(SystemPath path)
        {
            var root = new DirectoryInfo(path.Path);
            var projects =
                root.EnumerateDirectories("*", SearchOption.AllDirectories)
                    .Where(di => di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).Any())
                    .Select(di => new ProjectFiles
                    {
                        Directory = di,
                        CsprojFile = di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).FirstOrDefault(),
                        PackagesFile =
                            new PackagesConfig(di.GetFiles("packages.config", SearchOption.TopDirectoryOnly).FirstOrDefault())
                    });
            return projects;
        }

        private IEnumerable<Package> CreatePackages(PackagesConfig packagesConfig)
        {
            return packagesConfig.ExtractPackages();
        }
    }
}