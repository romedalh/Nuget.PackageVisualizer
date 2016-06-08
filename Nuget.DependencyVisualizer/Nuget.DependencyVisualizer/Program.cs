using System;
using System.Collections.Generic;
using Nuget.DependencyVisualizer.Core.Creator;
using Nuget.DependencyVisualizer.Core.Domain;

namespace Nuget.DependencyVisualizer.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var systemPath = new SystemPath(Configuration.Default.SystemPath);
            var system = new SystemCreator().CreateFromPath(systemPath);
            var packageToFind = new Package(Configuration.Default.PackageToFind);
            var projectsWithInstalledPackage = system.FindProjectsWithInstalledPackage(packageToFind);
            DisplayProjects(projectsWithInstalledPackage);

            Console.ReadKey();
        }

        private static void DisplayProjects(IEnumerable<Project> projectsWithInstalledPackage)
        {
            foreach (var project in projectsWithInstalledPackage)
            {
                Console.WriteLine(project.Name);
            }
        }
    }
}
