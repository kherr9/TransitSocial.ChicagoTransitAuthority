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

        #region "GetVehicles"

        [TestMethod]
        public void TestDeserializeGetVehicles()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetVehiclesResponse);

            // Act
            var response = this.serializer.Deserialize<GetVehiclesResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Vehicles);
            Assert.AreEqual(2, response.Vehicles.Length);
            var v1 = response.Vehicles[0];
            Assert.AreEqual("509", v1.VehicleId);
            Assert.AreEqual("20090611 10:28", v1.TimeStamp);
            Assert.AreEqual(41.92124938964844, v1.Latitude);
            Assert.AreEqual(-87.64849853515625, v1.Longitude);
            Assert.AreEqual(358, v1.Heading);
            Assert.AreEqual(3630, v1.PatternId);
            Assert.AreEqual(5678, v1.PatternDistance);
            Assert.AreEqual("8", v1.Route);
            Assert.AreEqual("Waveland/Broadway", v1.Destination);
            Assert.AreEqual(false, v1.IsDelayed);
            var v2 = response.Vehicles[1];
            Assert.AreEqual("392", v2.VehicleId);
            Assert.AreEqual("20090611 10:28", v2.TimeStamp);
            Assert.AreEqual(41.91095733642578, v2.Latitude);
            Assert.AreEqual(-87.64120713719782, v2.Longitude);
            Assert.AreEqual(88, v2.Heading);
            Assert.AreEqual(1519, v2.PatternId);
            Assert.AreEqual(11203, v2.PatternDistance);
            Assert.AreEqual("72", v2.Route);
            Assert.AreEqual("Clark", v2.Destination);
            Assert.AreEqual(false, v2.IsDelayed);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeGetVehiclesInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetVehiclesResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetVehiclesResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Vehicles);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Length);
            var error = response.Errors[0];
            Assert.AreEqual("Invalid API access key supplied", error.Message);
            Assert.IsNull(error.Route);
            Assert.IsNull(error.VehicleId);
        }

        #endregion

        #region "GetRoutes"

        [TestMethod]
        public void TestDeserializeGetRoutes()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetRoutesResponse);

            // Act
            var response = this.serializer.Deserialize<GetRoutesResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Routes);
            Assert.AreEqual(4, response.Routes.Length);
            var r1 = response.Routes[0];
            Assert.AreEqual("1", r1.RouteId);
            Assert.AreEqual("Indiana/Hyde Park", r1.Name);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeGetRoutesInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetRoutesResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetRoutesResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Routes);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Length);
            var error = response.Errors[0];
            Assert.AreEqual("Invalid API access key supplied", error.Message);
        }

        #endregion
    }
}
