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

        #region "BusTimeResponse"

        [TestMethod]
        public void TestDeserializeBusTimeResponse()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetTimeResponse);

            // Act
            var response = this.serializer.Deserialize<BusTimeResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("20090611 14:42:32", response.Time);
            Assert.IsNull(response.Error);
        }

        [TestMethod]
        public void TestDeserializeBusTimeResponseInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetTimeResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<BusTimeResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Time);
            Assert.IsNotNull(response.Error);
            Assert.AreEqual(1, response.Error.Count());
            Assert.AreEqual("Invalid API access key supplied", response.Error.Single().Message);
        }

        #endregion
    }
}
