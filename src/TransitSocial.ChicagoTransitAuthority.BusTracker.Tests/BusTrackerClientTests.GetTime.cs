using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        [TestMethod]
        public void TestGetTimeInvalidApiToken()
        {
            // Arrange
            const string InvalidApiToken = "INVALID_API_TOKEN";
            var client = new BusTrackerClient(UrlBase, InvalidApiToken);

            var expectedModel = this.repository.GetAs<GetTimeResponse>(ResourceFiles.GetTimeResponseInvalidApiAccess);

            Exception exception = null;

            // Act
            StartOwinTest(
                () =>
                {
                    try
                    {
                        var time = client.GetTime();
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
        public void TestGetTime()
        {
            // Arrange
            var client = new BusTrackerClient(UrlBase, WebAppConfig.ApiKey);

            var expectedModel = this.repository.GetAs<GetTimeResponse>(ResourceFiles.GetTimeResponse);

            string time = null;

            // Act
            StartOwinTest(
                () =>
                {
                    time = client.GetTime();

                    return Task.FromResult(true);
                });

            // Assert
            Assert.AreEqual(time, expectedModel.Time);
        }

    }
}
