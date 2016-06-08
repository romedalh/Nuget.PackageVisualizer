using System.Collections.Generic;
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
            var packages = new Package[]
            {
                new Package("test.package1"),
                new Package("test.package2"),
                new Package("test.package3")
            };
            var project = new Project("test.project",packages);

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
            var packages1 = new Package[]
            {
                new Package("package1"),
                new Package("package2")
            };
            var project1 = new Project("proj1", packages1);

            var packages2 = new Package[]
            {
                new Package("another package")
            };
            var project2 = new Project("proj2", packages2);
            project2.InstallPackage(new Package("another package"));

            var packages3 = new Package[]
            {
                new Package("another package"),
                new Package("another package 2"),
                new Package("package2")
            };
            var project3 = new Project("proj3", packages3);

            var project4 = new Project("proj4", new List<Package>());

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
