using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        [TestMethod]
        public void TestGetVehiclesInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var vehicles = client.GetVehicles(Enumerable.Empty<string>(), Enumerable.Empty<string>());
                        Assert.Fail("Exception thrown exception");
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }

                    return Task.FromResult(true);
                });

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedModel.Errors.Select(e => e.Message).First(), exception.Message);
        }

        [TestMethod]
        public void TestGetVehicles()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponse);

            IEnumerable<Vehicle> vehicles = null;

            // Act
            StartOwinTest(
                () =>
                {
                    vehicles = client.GetVehicles(Enumerable.Empty<string>(), Enumerable.Empty<string>());

                    return Task.FromResult(true);
                });

            // Assert
            var expectedVehicleIds = expectedModel.Vehicles.Select(v => v.VehicleId).ToList();
            var actualVehicleIds = vehicles.Select(v => v.VehicleId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }
    }
}
