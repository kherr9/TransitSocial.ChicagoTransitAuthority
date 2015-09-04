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
        public IEnumerable<Stop> GetStops(string routeId, string direction)
        {
            var request = this.CreateRequest("/bustime/api/v1/getstops", CreateGetStopsQueryString(routeId, direction));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetStopsResponse getStopsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getStopsResponse = this.Serializer.Deserialize<GetStopsResponse>(responseStream);
                }

                if (getStopsResponse.Errors != null && getStopsResponse.Errors.Any())
                {
                    throw new Exception(getStopsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getStopsResponse.Stops;
                }
            }
        }

        public Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction)
        {
            return this.GetStopsAsync(routeId, direction, CancellationToken.None);
        }

        public async Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction, CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/getstops", CreateGetStopsQueryString(routeId, direction));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetStopsResponse getStopsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getStopsResponse = this.Serializer.Deserialize<GetStopsResponse>(responseStream);
                }

                if (getStopsResponse.Errors != null && getStopsResponse.Errors.Any())
                {
                    throw new Exception(getStopsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getStopsResponse.Stops;
                }
            }
        }

        internal static NameValueCollection CreateGetStopsQueryString(string routeId, string direction)
        {
            var queryString = CreateQueryStringCollection();

            queryString.Add("rt", routeId);

            queryString.Add("dir", direction);

            return queryString;
        }
    }
}
