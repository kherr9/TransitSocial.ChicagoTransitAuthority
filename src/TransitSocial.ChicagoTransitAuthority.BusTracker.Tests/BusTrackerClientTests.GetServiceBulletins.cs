using System;
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
        #region "GetServiceBulletins"

        [TestMethod]
        public void TestGetServiceBulletinsInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var serviceBulletins = client.GetServiceBulletins(Enumerable.Empty<string>(), null, Enumerable.Empty<string>());
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
        public void TestGetServiceBulletins()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponse);

            IEnumerable<ServiceBulletin> serviceBulletins = null;

            // Act
            StartOwinTest(
                () =>
                {
                    serviceBulletins = client.GetServiceBulletins(Enumerable.Empty<string>(), null, Enumerable.Empty<string>());

                    return Task.FromResult(true);
                });

            // Assert
            var expectedServiceBulletinNames = expectedModel.ServiceBulletins.Select(x => x.Name).ToList();
            var actualServiceBulletinNames = serviceBulletins.Select(x => x.Name).ToList();

            CollectionAssert.AreEqual(expectedServiceBulletinNames, actualServiceBulletinNames);
        }

        #endregion

        #region "GetServiceBulletinsAsync"

        [TestMethod]
        public void TestGetServiceBulletinsAsyncInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>());
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
        public void TestGetServiceBulletinsAsyncInvalidApiTokenWithCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponseInvalidApiAccess);

            Exception exception = null;

            var cts = new CancellationTokenSource();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>(), cts.Token);
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
        public void TestGetServiceBulletinsAsyncInvalidApiTokenWithCancelledCancellationToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponseInvalidApiAccess);

            OperationCanceledException exception = null;

            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>(), cts.Token);
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
        public void TestGetServiceBulletinsAsync()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponse);

            IEnumerable<ServiceBulletin> serviceBulletins = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>());
                });

            // Assert
            var expectedServiceBulletinNames = expectedModel.ServiceBulletins.Select(x => x.Name).ToList();
            var actualServiceBulletinNames = serviceBulletins.Select(x => x.Name).ToList();

            CollectionAssert.AreEqual(expectedServiceBulletinNames, actualServiceBulletinNames);
        }

        [TestMethod]
        public void TestGetServiceBulletinsAsyncWithCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponse);

            var cts = new CancellationTokenSource();

            IEnumerable<ServiceBulletin> serviceBulletins = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>(), cts.Token);
                });

            // Assert
            var expectedServiceBulletinNames = expectedModel.ServiceBulletins.Select(x => x.Name).ToList();
            var actualServiceBulletinNames = serviceBulletins.Select(x => x.Name).ToList();

            CollectionAssert.AreEqual(expectedServiceBulletinNames, actualServiceBulletinNames);
        }

        [TestMethod]
        public void TestGetServiceBulletinsAsyncWithCancelledCancellationToken()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetServiceBulletinResponse>(ResourceFiles.GetServiceBulletinsResponse);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            OperationCanceledException exception = null;

            // Act
            StartOwinTest(
                async () =>
                {
                    try
                    {
                        var serviceBulletins = await client.GetServiceBulletinsAsync(Enumerable.Empty<string>(), null, Enumerable.Empty<string>(), cts.Token);
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

        #region "CreateGetServiceBulletinsQueryString"

        [TestMethod]
        public void TestCreateGetServiceBulletinsQueryStringWhenEmpty()
        {
            // Arrange
            var routeIds = Enumerable.Empty<string>();
            string routeDirection = null;
            var stopIds = Enumerable.Empty<string>();

            // Act
            var collection = BusTrackerClient.CreateGetServiceBulletinsQueryString(routeIds, routeDirection, stopIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetServiceBulletinsQueryStringWhenNull()
        {
            // Arrange
            IEnumerable<string> routeIds = null;
            string routeDirection = null;
            IEnumerable<string> stopIds = null;

            // Act
            var collection = BusTrackerClient.CreateGetServiceBulletinsQueryString(routeIds, routeDirection, stopIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("", collection.ToString());
        }

        [TestMethod]
        public void TestCreateGetServiceBulletinsQueryString()
        {
            // Arrange
            IEnumerable<string> routeIds = new[] { "1123 J" };
            string routeDirection = "North Bound";
            IEnumerable<string> stopIds = new[] { "123", "123 H" };

            // Act
            var collection = BusTrackerClient.CreateGetServiceBulletinsQueryString(routeIds, routeDirection, stopIds);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual("rt=1123+J&rtdir=North+Bound&stpid=123%2c123+H", collection.ToString());
        }

        #endregion
    }
}
