using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    public partial class BusTrackerClientTests
    {
        #region "GetServiceBulletins"

        [TestMethod]
        public void TestFooWhenCondition()
        {
            // Arrange

            // Act

            // Assert
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
