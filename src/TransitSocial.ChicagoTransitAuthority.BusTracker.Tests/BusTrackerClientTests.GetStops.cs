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
        #region "GetStops"

        [TestMethod]
        public void TestGetStopsInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var stops = client.GetStops(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
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
        public void TestGetStops()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponse);

            IEnumerable<Stop> stops = null;

            // Act
            StartOwinTest(
                () =>
                {
                    stops = client.GetStops(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                    return Task.FromResult(true);
                });

            // Assert
            var expectedStopIds = expectedModel.Stops.Select(x => x.StopId).ToList();
            var actualStopIds = stops.Select(x => x.StopId).ToList();

            CollectionAssert.AreEqual(expectedStopIds, actualStopIds);
        }

        #endregion

        #region "GetStopsAsync"

        [TestMethod]
        public void TestGetStopsAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
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
        public void TestGetStopsAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), cts.Token);
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
        public void TestGetStopsAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponseInvalidApiAccess);

            OperationCanceledException exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), cts.Token);
                        Assert.Fail("Exception thrown exception");
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
        public void TestGetStopsAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponse);

            IEnumerable<Stop> stops = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                });

            // Assert
            var expectedStopIds = expectedModel.Stops.Select(x => x.StopId).ToList();
            var actualStopIds = stops.Select(x => x.StopId).ToList();

            CollectionAssert.AreEqual(expectedStopIds, actualStopIds);
        }

        [TestMethod]
        public void TestGetStopsAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponse);

            var cts = new CancellationTokenSource();

            IEnumerable<Stop> stops = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), cts.Token);
                });

            // Assert
            var expectedStopIds = expectedModel.Stops.Select(x => x.StopId).ToList();
            var actualStopIds = stops.Select(x => x.StopId).ToList();

            CollectionAssert.AreEqual(expectedStopIds, actualStopIds);
        }

        [TestMethod]
        public void TestGetStopsAsyncWithCancelledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetStopsResponse>(ResourceFiles.GetStopsResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var stops = await client.GetStopsAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), cts.Token);
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

        #region "CreateGetStopsQueryString"

        [TestMethod]
        public void TestCreateGetStopsQueryString()
        {
            // Arrange
            var routeId = "88 N";
            var direction = "North Bound";

            // Act
            var collection = BusTrackerClient.CreateGetStopsQueryString(routeId, direction);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=88+N&dir=North+Bound", collection.ToString());
        }

        #endregion
    }
}
