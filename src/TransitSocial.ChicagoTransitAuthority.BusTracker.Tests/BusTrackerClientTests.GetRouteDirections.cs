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
        #region "TestGetRouteDirections"

        [TestMethod]
        public void TestGetRouteDirectionsInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var directions = client.GetRouteDirections(Guid.NewGuid().ToString());
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
        public void TestGetRouteDirections()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponse);

            IEnumerable<string> directions = null;

            // Act
            StartOwinTest(
                () =>
                {
                    directions = client.GetRouteDirections(Guid.NewGuid().ToString());

                    return Task.FromResult(true);
                });

            // Assert
            var expectedDirections = expectedModel.Directions.ToList();
            var actualDirections = directions.ToList();

            CollectionAssert.AreEqual(expectedDirections, actualDirections);
        }

        #endregion

        #region "TestGetRouteDirectionsAsync"

        [TestMethod]
        public void TestGetRouteDirectionsAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString());
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
        public void TestGetRouteDirectionsAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString(), cts.Token);
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
        public void TestGetRouteDirectionsAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString(), cts.Token);
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
        public void TestGetRouteDirectionsAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponse);

            IEnumerable<string> directions = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString());
                });

            // Assert
            var expectedDirections = expectedModel.Directions.ToList();
            var actualDirections = directions.ToList();

            CollectionAssert.AreEqual(expectedDirections, actualDirections);
        }

        [TestMethod]
        public void TestGetRouteDirectionsAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponse);

            var cts = new CancellationTokenSource();

            IEnumerable<string> directions = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString(), cts.Token);
                });

            // Assert
            var expectedDirections = expectedModel.Directions.ToList();
            var actualDirections = directions.ToList();

            CollectionAssert.AreEqual(expectedDirections, actualDirections);
        }

        [TestMethod]
        public void TestGetRouteDirectionsAsyncWithCanceledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetRouteDirectionsResponse>(ResourceFiles.GetRouteDirectionsResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var directions = await client.GetRouteDirectionsAsync(Guid.NewGuid().ToString(), cts.Token);
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

        #region "CreateGetRouteDirectionsQueryString"

        [TestMethod]
        public void TestCreateGetRouteDirectionsQueryString()
        {
            // Arrange
            var routeId = "88 N";

            // Act
            var collection = BusTrackerClient.CreateGetRouteDirectionsQueryString(routeId);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=88+N", collection.ToString());
        }

        #endregion

    }
}
