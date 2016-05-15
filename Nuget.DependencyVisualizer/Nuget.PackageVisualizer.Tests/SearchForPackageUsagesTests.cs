using System.Linq;
using Nuget.DependencyVisualizer.Core;
using Nuget.DependencyVisualizer.Core.Domain;
using NUnit.Framework;

namespace Nuget.PackageVisualizer.Tests
{
    [TestFixture]
    public class SearchForPackageUsagesTests
    {  
        [Test]
        public void Can_Find_Package_In_Single_Project()
        {
            //ARRANGE
            var project = new Project("test.project");
            project.InstallPackage(new Package("test.package1"));
            project.InstallPackage(new Package("test.package2"));
            project.InstallPackage(new Package("test.package3"));

            //ACT
            var isInstalled = project.HasPackageInstalled(new Package("test.package2"));
            var isInstalled2 = project.HasPackageInstalled(new Package("another.package"));

            //ASSERT
            Assert.That(isInstalled,Is.True,"package should be included inside project");
            Assert.That(isInstalled2, Is.False, "package should not be included inside project");
        }

        [Test]
        public void Can_Find_Package_Across_Whole_System()
        {
            //ARRANGE
            var project1 = new Project("proj1");
            project1.InstallPackage(new Package("package1"));
            project1.InstallPackage(new Package("package2"));

            var project2 = new Project("proj2");
            project2.InstallPackage(new Package("another package"));

            var project3 = new Project("proj3");
            project3.InstallPackage(new Package("another package"));
            project3.InstallPackage(new Package("another package 2"));
            project3.InstallPackage(new Package("package2"));

            var project4 = new Project("proj3");

             var system = new DependencyVisualizer.Core.Domain.System(new []{project1,project2,project3,project4});
            //ACT
            var projectsWithPackage = system.FindProjectsWithInstalledPackage(new Package("package2")).ToList();

            //ASSERT
            Assert.That(projectsWithPackage.Count,Is.EqualTo(2));
            Assert.True(projectsWithPackage.Contains(project1));
            Assert.True(projectsWithPackage.Contains(project3));
        }
    }
}
