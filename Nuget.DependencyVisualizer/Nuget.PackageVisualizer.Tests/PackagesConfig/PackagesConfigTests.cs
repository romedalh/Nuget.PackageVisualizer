using System;
using System.Collections.Generic;
using System.IO;
using Nuget.DependencyVisualizer.Core.Domain;
using Nuget.DependencyVisualizer.Core.Exceptions;
using NUnit.Framework;

namespace Nuget.PackageVisualizer.Tests.PackagesConfig
{
    [TestFixture]
    public class PackagesConfigTests
    {
        [Test]
        public void Reads_Packages_From_File()
        {
            //ARRANGE
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PackagesConfig/TestCases/standard.config");
            var fileInfo = new FileInfo(path);
            var packagesConfig = new DependencyVisualizer.Core.Creator.Files.PackagesConfig(fileInfo);
            //ACT
            var packages = packagesConfig.ExtractPackages();

            //ASSERT
            var expectedPackages = new List<Package>
            {
                new Package("NUnit"),
                new Package("Nuget.DependencyVisualizer.Core"),
                new Package("System.Data.TestCase")
            };
            Assert.That(packages,Is.EquivalentTo(expectedPackages));
        }

        [Test]
        public void Should_Return_Empty_Collection_When_No_Packages_In_File()
        {
            //ARRANGE
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PackagesConfig/TestCases/noEntries.config");
            var fileInfo = new FileInfo(path);
            var packagesConfig = new DependencyVisualizer.Core.Creator.Files.PackagesConfig(fileInfo);
            //ACT
            var packages = packagesConfig.ExtractPackages();

            //ASSERT
            var expectedPackages = new List<Package>();
            Assert.That(packages, Is.EquivalentTo(expectedPackages));
        }

        [Test]
        public void Should_Throw_Exception_When_File_Is_Not_Xml()
        {
            //ARRANGE
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PackagesConfig/TestCases/noXml.config");
            var fileInfo = new FileInfo(path);
            var packagesConfig = new DependencyVisualizer.Core.Creator.Files.PackagesConfig(fileInfo);

            //ACT & ASSERT
            Assert.Throws<IncorrectFileException>(()=>packagesConfig.ExtractPackages());
        }

        [Test]
        public void Should_Return_Empty_List_When_File_Not_Exists()
        {
            //ARRANGE
            var packagesConfig = new DependencyVisualizer.Core.Creator.Files.PackagesConfig(null);

            //ACT
            var packages = packagesConfig.ExtractPackages();

            //ASSERT
            var expectedPackages = new List<Package>();
            Assert.That(packages, Is.EquivalentTo(expectedPackages));
        }
    }
}