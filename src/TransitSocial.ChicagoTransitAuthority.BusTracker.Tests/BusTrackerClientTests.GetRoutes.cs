using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        #region "GetRoutes"

        [TestMethod]
        public void TestGetRoutesInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var routes = client.GetRoutes();
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
        public void TestGetRoutes()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponse);

            IEnumerable<Route> routes = null;

            // Act
            StartOwinTest(
                () =>
                {
                    routes = client.GetRoutes();

                    return Task.FromResult(true);
                });

            // Assert
            var expectedVehicleIds = expectedModel.Routes.Select(v => v.RouteId).ToList();
            var actualVehicleIds = routes.Select(v => v.RouteId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }

        #endregion

        #region "GetRouteAsync"

        [TestMethod]
        public void TestGetRoutesAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var routes = await client.GetRoutesAsync();
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
        public void TestGetRoutesAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var vehicles = await client.GetRoutesAsync(cts.Token);
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
        public void TestGetRoutesAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponseInvalidApiAccess);

            OperationCanceledException exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var time = await client.GetRoutesAsync(cts.Token);
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
        public void TestGetRoutesAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponse);

            Route[] routes = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    routes = (await client.GetRoutesAsync()).ToArray();
                });

            // Assert
            var expectedVehicleIds = expectedModel.Routes.Select(v => v.RouteId).ToList();
            var actualVehicleIds = routes.Select(v => v.RouteId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }

        [TestMethod]
        public void TestGetRoutesAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponse);

            var cts = new CancellationTokenSource();

            Route[] routes = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    routes = (await client.GetRoutesAsync(cts.Token)).ToArray();
                });

            // Assert
            var expectedVehicleIds = expectedModel.Routes.Select(v => v.RouteId).ToList();
            var actualVehicleIds = routes.Select(v => v.RouteId).ToList();

            CollectionAssert.AreEqual(expectedVehicleIds, actualVehicleIds);
        }

        [TestMethod]
        public void GetRoutesAsyncWithCancelledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRoutesResponse>(ResourceFiles.GetRoutesResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var routes = (await client.GetRoutesAsync(cts.Token)).ToArray();
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
    }
}
