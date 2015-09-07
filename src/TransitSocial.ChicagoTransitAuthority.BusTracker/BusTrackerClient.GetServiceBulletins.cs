using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public partial class BusTrackerClient
    {
        public IEnumerable<ServiceBulletin> GetServiceBulletins(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds)
        {
            var request = this.CreateRequest(
                "/bustime/api/v1/getservicebulletins",
                CreateGetServiceBulletinsQueryString(routeIds, routeDirection, stopIds));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetServiceBulletinResponse getServiceBulletinResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getServiceBulletinResponse = this.Serializer.Deserialize<GetServiceBulletinResponse>(responseStream);
                }

                if (getServiceBulletinResponse.Errors != null && getServiceBulletinResponse.Errors.Any())
                {
                    throw new Exception(getServiceBulletinResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getServiceBulletinResponse.ServiceBulletins;
                }
            }
        }

        public Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds)
        {
            return this.GetServiceBulletinsAsync(routeIds, routeDirection, stopIds, CancellationToken.None);
        }

        public async Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds, CancellationToken token)
        {
            var request = this.CreateRequest(
                "/bustime/api/v1/getservicebulletins",
                CreateGetServiceBulletinsQueryString(routeIds, routeDirection, stopIds));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetServiceBulletinResponse getServiceBulletinResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getServiceBulletinResponse = this.Serializer.Deserialize<GetServiceBulletinResponse>(responseStream);
                }

                if (getServiceBulletinResponse.Errors != null && getServiceBulletinResponse.Errors.Any())
                {
                    throw new Exception(getServiceBulletinResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getServiceBulletinResponse.ServiceBulletins;
                }
            }
        }

        internal static NameValueCollection CreateGetServiceBulletinsQueryString(
            IEnumerable<string> routeIds,
            string routeDirection,
            IEnumerable<string> stopIds)
        {
            var queryString = CreateQueryStringCollection();

            if (routeIds != null)
            {
                var value = string.Join(",", routeIds);

                if (value != string.Empty)
                {
                    queryString.Add("rt", value);
                }
            }

            if (routeDirection != null)
            {
                queryString.Add("rtdir", routeDirection);
            }

            if (stopIds != null)
            {
                var value = string.Join(",", stopIds);

                if (value != string.Empty)
                {
                    queryString.Add("stpid", value);
                }
            }

            return queryString;
        }
    }
}
