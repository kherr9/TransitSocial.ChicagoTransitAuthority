using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources
{
    [TestClass]
    public class ResourceRepositoryTests
    {
        [TestMethod]
        public void TestAllResourceFilesExist()
        {
            // Arrange
            var repository = new ResourceRepository();

            // Act
            foreach (var path in this.GetAllResourceFileKeys())
            {
                var data = repository.GetString(path);

                // Assert
                Assert.IsNotNull(data);
            }
        }

        [TestMethod]
        public void TestGetallResourceFileKeysIsNotEmpty()
        {
            // Arrange

            // Act
            var keys = this.GetAllResourceFileKeys();

            // Assert
            Assert.AreEqual(true, keys.Any());
        }

        private IEnumerable<string> GetAllResourceFileKeys()
        {
            var fieldInfos = typeof(ResourceFiles).GetFields(
                // Gets all public and static fields

                BindingFlags.Public | BindingFlags.Static |
                // This tells it to get the fields from all base types as well

                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach (var fi in fieldInfos)
            {
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determine if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if (fi.IsLiteral && !fi.IsInitOnly)
                {
                    yield return (string)fi.GetValue(null);
                }
            }
        }
    }
}
