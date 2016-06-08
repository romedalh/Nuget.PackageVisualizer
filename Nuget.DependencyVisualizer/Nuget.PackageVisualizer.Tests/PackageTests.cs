using System.IO;
using Nuget.DependencyVisualizer.Core;
using Nuget.DependencyVisualizer.Core.Domain;
using NUnit.Framework;

namespace Nuget.PackageVisualizer.Tests
{
    [TestFixture]
    public class PackageTests
    {
        
        [TestCase("package1","package1")]
        [TestCase("PackagE.1","pACkage.1")]
        public void Returns_True_When_Two_Packages_Have_Same_Name(string firstName, string secondName)
        {
            //ARRANGE
            var first = new Package(firstName);
            var second = new Package(secondName);

            //ACT
            var areEqual = first.Equals(second);

            //ASSERT
            Assert.True(areEqual);
        }

        [TestCase("package1", "package.1")]
        [TestCase("some.package", "some.")]
        public void Returns_True_When_Two_Packages_Have_Different_Name(string firstName, string secondName)
        {
            //ARRANGE
            var first = new Package(firstName);
            var second = new Package(secondName);

            //ACT
            var areEqual = first.Equals(second);

            //ASSERT
            Assert.False(areEqual);
        }
    }
}