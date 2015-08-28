using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    [TestClass]
    public class SerializerTests
    {
        private ResourceRepository repository;

        private Serializer serializer;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = new ResourceRepository();
            this.serializer = new Serializer();
        }

        #region "GetTimeResponse"

        [TestMethod]
        public void TestDeserializeBusTimeResponse()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetTimeResponse);

            // Act
            var response = this.serializer.Deserialize<GetTimeResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("20090611 14:42:32", response.Time);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeBusTimeResponseInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetTimeResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetTimeResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Time);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Count());
            Assert.AreEqual("Invalid API access key supplied", response.Errors.Single().Message);
        }

        #endregion
    }
}
