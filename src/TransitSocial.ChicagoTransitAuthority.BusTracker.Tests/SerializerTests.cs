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

        #region "GetRouteDirections"

        [TestMethod]
        public void TestDeserializeGetRouteDirections()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetRouteDirectionsResponse);

            // Act
            var response = this.serializer.Deserialize<GetRouteDirectionsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Directions);
            Assert.AreEqual(2, response.Directions.Length);
            Assert.AreEqual("Northbound", response.Directions[0]);
            Assert.AreEqual("Southbound", response.Directions[1]);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeGetRouteDirectionsInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetRouteDirectionsResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetRouteDirectionsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Directions);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Length);
            var error = response.Errors[0];
            Assert.AreEqual("Invalid API access key supplied", error.Message);
        }

        #endregion

        #region "GetStops"

        [TestMethod]
        public void TestDeserializeGetStops()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetStopsResponse);

            // Act
            var response = this.serializer.Deserialize<GetStopsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Stops);
            Assert.AreEqual(4, response.Stops.Length);
            var s1 = response.Stops[0];
            Assert.AreEqual(s1.StopId, 4727);
            Assert.AreEqual(s1.Name, "1633 W Madison");
            Assert.AreEqual(s1.Latitude, 41.881265);
            Assert.AreEqual(s1.Longitude, -87.66849);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeGetStopsInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetStopsResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetStopsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Stops);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Length);
            var error = response.Errors[0];
            Assert.AreEqual("Invalid API access key supplied", error.Message);
        }

        #endregion

        #region "GetPredictions"

        [TestMethod]
        public void TestDeserializeGetPredictions()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetPredictionsResponse);

            // Act
            var response = this.serializer.Deserialize<GetPredictionsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Predictions);
            Assert.AreEqual(2, response.Predictions.Length);
            var p1 = response.Predictions[0];
            Assert.AreEqual(p1.Timestamp, "20150904 14:43");
            Assert.AreEqual(p1.Type, "A");
            Assert.AreEqual(p1.StopId, 456);
            Assert.AreEqual(p1.StopName, "Madison & Jefferson");
            Assert.AreEqual(p1.VehicleId, 1761);
            Assert.AreEqual(p1.Distance, 8950);
            Assert.AreEqual(p1.RouteId, "20");
            Assert.AreEqual(p1.RouteDirection, "Westbound");
            Assert.AreEqual(p1.Destination, "Austin");
            Assert.AreEqual(p1.PredictionTime, "20150904 15:00");
            Assert.AreEqual(p1.IsDelayed, false);
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void TestDeserializeGetPredictionsInvalidApiAccess()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetPredictionsResponseInvalidApiAccess);

            // Act
            var response = this.serializer.Deserialize<GetPredictionsResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Predictions);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Length);
            var error = response.Errors[0];
            Assert.AreEqual("Invalid API access key supplied", error.Message);
        }

        #endregion

        #region "GetServiceBulletins"

        [TestMethod]
        public void TestDeserializeGetServiceBulletins()
        {
            // Arrange
            var xml = this.repository.GetString(ResourceFiles.GetServiceBulletinsResponse);

            // Act
            var response = this.serializer.Deserialize<GetServiceBulletinResponse>(xml);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ServiceBulletins);
            Assert.AreEqual(9, response.ServiceBulletins.Length);
            var sb1 = response.ServiceBulletins[0];
            Assert.AreEqual(sb1.Name, "#18 - Cosntruction");
            Assert.AreEqual(sb1.Subject, "#18 16th/18th Reroute");
            Assert.AreEqual(sb1.Detail, "Buses will operate in both directions via 16th, Western, 15th, Rockwell, and 16th.<br/><br/>Allow extra travel time.<br/><br/>Buses are rerouted due to street repair work on 16th Street between 2400 West and 2457 West.<br/>");
            Assert.AreEqual(sb1.Brief, "");
            Assert.AreEqual(sb1.Priority, "Low");
            Assert.AreEqual(sb1.AffectedServices, null);
            var sb9 = response.ServiceBulletins[8];
            Assert.IsNotNull(sb9.AffectedServices);
            Assert.AreEqual(7, sb9.AffectedServices.Length);
            var sb9_as1 = sb9.AffectedServices[0];
            Assert.AreEqual("J14", sb9_as1.RouteId);
            Assert.AreEqual("Northbound", sb9_as1.RouteDirection);
            Assert.IsNull(response.Errors);
        }

        #endregion
    }
}
