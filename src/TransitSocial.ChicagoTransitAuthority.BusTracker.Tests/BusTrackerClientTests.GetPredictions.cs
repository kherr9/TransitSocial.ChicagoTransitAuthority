using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        #region "GetPredictions"

        [TestMethod]
        public void TestGetPredictionsInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var predictions = client.GetPredictions(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null);
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
        public void TestGetPredictions()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponse);

            IEnumerable<Prediction> predictions = null;

            // Act
            StartOwinTest(
                () =>
                {
                    predictions = client.GetPredictions(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null);

                    return Task.FromResult(true);
                });

            // Assert
            var expectedPredictionTimes = expectedModel.Predictions.Select(x => x.Timestamp).ToList();
            var actualPredictionTimes = predictions.Select(v => v.Timestamp).ToList();

            CollectionAssert.AreEqual(expectedPredictionTimes, actualPredictionTimes);
        }

        #endregion

        #region "GetPredictionsAsync"

        [TestMethod]
        public void TestGetPredictionsAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null);
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
        public void TestGetPredictionsAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, cts.Token);
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
        public void TestGetPredictionsAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponseInvalidApiAccess);

            OperationCanceledException exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, cts.Token);
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
        public void TestGetPredictionsAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponse);

            IEnumerable<Prediction> predictions = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null);
                });

            // Assert
            var expectedPredictionTimes = expectedModel.Predictions.Select(x => x.Timestamp).ToList();
            var actualPredictionTimes = predictions.Select(v => v.Timestamp).ToList();

            CollectionAssert.AreEqual(expectedPredictionTimes, actualPredictionTimes);
        }

        [TestMethod]
        public void TestGetPredictionsAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponse);

            var cts = new CancellationTokenSource();

            IEnumerable<Prediction> predictions = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, cts.Token);
                });

            // Assert
            var expectedPredictionTimes = expectedModel.Predictions.Select(x => x.Timestamp).ToList();
            var actualPredictionTimes = predictions.Select(v => v.Timestamp).ToList();

            CollectionAssert.AreEqual(expectedPredictionTimes, actualPredictionTimes);
        }

        [TestMethod]
        public void TestGetPredictionsAsyncWithCancelledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetPredictionsResponse>(ResourceFiles.GetPredictionsResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var predictions = await client.GetPredictionsAsync(Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, cts.Token);
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

        #region "CreateGetPredictionsQueryString"

        [TestMethod]
        public void TestCreateGetPredictionsQueryStringWhenEmpty()
        {
            // Arrange
            var stopIds = Enumerable.Empty<string>();
            var routeIds = Enumerable.Empty<string>();
            var vehicleIds = Enumerable.Empty<string>();
            int? top = null;

            // Act
            var collection = BusTrackerClient.CreateGetPredictionsQueryString(stopIds, routeIds, vehicleIds, top);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetPredictionsQueryStringWhenNull()
        {
            // Arrange
            IEnumerable<string> stopIds = null;
            IEnumerable<string> routeIds = null;
            IEnumerable<string> vehicleIds = null;
            int? top = null;

            // Act
            var collection = BusTrackerClient.CreateGetPredictionsQueryString(stopIds, routeIds, vehicleIds, top);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetPredictionsQueryString()
        {
            // Arrange
            IEnumerable<string> stopIds = null;
            IEnumerable<string> routeIds = new[] { "apple jacks" };
            IEnumerable<string> vehicleIds = new[] { "foo", "bar" }; ;
            int? top = 343;

            // Act
            var collection = BusTrackerClient.CreateGetPredictionsQueryString(stopIds, routeIds, vehicleIds, top);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=apple+jacks&vid=foo%2cbar&top=343", collection.ToString());
        }

        #endregion
    }
}
