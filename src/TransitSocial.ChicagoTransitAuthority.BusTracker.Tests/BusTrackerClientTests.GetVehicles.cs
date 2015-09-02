using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        #region "GetVehicles"

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

        #endregion

        #region "GetVehiclesAsync"

        [TestMethod]
        public void TestGetVehiclesAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var vehicles = await client.GetVehiclesAsync(null, null);
                        Assert.Fail("Exception thrown exception");
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                });

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedModel.Errors.Select(e => e.Message).First(), exception.Message);
        }

        [TestMethod]
        public void TestGetVehiclesAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var vehicles = await client.GetVehiclesAsync(null, null, cts.Token);
                        Assert.Fail("Exception thrown exception");
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                });

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedModel.Errors.Select(e => e.Message).First(), exception.Message);
        }

        [TestMethod]
        public void TestGetVehiclesAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponseInvalidApiAccess);

            OperationCanceledException exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var time = await client.GetVehiclesAsync(null, null, cts.Token);
                        Assert.Fail();
                    }
                    catch (OperationCanceledException ex)
                    {
                        exception = ex;
                    }
                });

            // Assert
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestGetVehiclesAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponse);

            Vehicle[] vehicles = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    vehicles = (await client.GetVehiclesAsync(null, null)).ToArray();
                });

            // Assert
            var expectedVehicleIds = expectedModel.Vehicles.Select(v => v.VehicleId).ToList();
            var actualVehicleIds = vehicles.Select(v => v.VehicleId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }

        [TestMethod]
        public void TestGetVehiclesAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponse);

            var cts = new CancellationTokenSource();

            Vehicle[] vehicles = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    vehicles = (await client.GetVehiclesAsync(null, null, cts.Token)).ToArray();
                });

            // Assert
            var expectedVehicleIds = expectedModel.Vehicles.Select(v => v.VehicleId).ToList();
            var actualVehicleIds = vehicles.Select(v => v.VehicleId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }

        [TestMethod]
        public void GetVehiclesAsyncWithCancelledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetVehiclesResponse>(ResourceFiles.GetVehiclesResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var vehicles = (await client.GetVehiclesAsync(null, null, cts.Token)).ToArray();
                        Assert.Fail();
                    }
                    catch (OperationCanceledException ex)
                    {
                        exception = ex;
                    }
                });

            // Assert
            Assert.IsNotNull(exception);
        }

        #endregion

        #region "CreateGetVehiclesQueryString"

        [TestMethod]
        public void TestCreateGetVehiclesQueryStringWhenEmpty()
        {
            // Arrange
            var vehicledIds = Enumerable.Empty<string>();
            var routeIds = Enumerable.Empty<string>();

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenVehiclesIsNull()
        {
            // Arrange
            IEnumerable<string> vehicledIds = null;
            var routeIds = Enumerable.Empty<string>();

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenSingleVehicleId()
        {
            // Arrange
            var vehicledIds = new[]
                                  {
                                      "123123"
                                  };
            var routeIds = Enumerable.Empty<string>();

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("vid=123123", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenMultipleVehicleId()
        {
            // Arrange
            var vehicledIds = new[]
                                  {
                                      "1111",
                                      "2222",
                                      "N 333"
                                  };
            var routeIds = Enumerable.Empty<string>();

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("vid=1111%2c2222%2cN+333", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenRoutesIsNull()
        {
            // Arrange
            IEnumerable<string> vehicledIds = Enumerable.Empty<string>();
            IEnumerable<string> routeIds = null;

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenSingleRouteId()
        {
            // Arrange
            var vehicledIds = Enumerable.Empty<string>();
            var routeIds = new[]
                                  {
                                      "123123"
                                  };

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=123123", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetVehiclesQueryWhenMultipleRouteId()
        {
            // Arrange
            var vehicledIds = Enumerable.Empty<string>();
            var routeIds = new[]
                                  {
                                      "1111",
                                      "2222",
                                      "N 333"
                                  };

            // Act
            var collection = BusTrackerClient.CreateGetVehiclesQueryString(vehicledIds, routeIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=1111%2c2222%2cN+333", collection.ToString());
        }

        #endregion
    }
}
