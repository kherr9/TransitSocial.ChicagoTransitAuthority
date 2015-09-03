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
        public IEnumerable<string> GetRouteDirections(string routeId)
        {
            var request = this.CreateRequest("/bustime/api/v1/getdirections", CreateGetRouteDirectionsQueryString(routeId));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetRouteDirectionsResponse getRouteDirectionsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getRouteDirectionsResponse = this.Serializer.Deserialize<GetRouteDirectionsResponse>(responseStream);
                }

                if (getRouteDirectionsResponse.Errors != null && getRouteDirectionsResponse.Errors.Any())
                {
                    throw new Exception(getRouteDirectionsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getRouteDirectionsResponse.Directions;
                }
            }
        }

        public Task<IEnumerable<string>> GetRouteDirectionsAsync(string routeId)
        {
            return this.GetRouteDirectionsAsync(routeId, CancellationToken.None);
        }

        public async Task<IEnumerable<string>> GetRouteDirectionsAsync(string routeId, CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/getdirections", CreateGetRouteDirectionsQueryString(routeId));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetRouteDirectionsResponse getRouteDirectionsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getRouteDirectionsResponse = this.Serializer.Deserialize<GetRouteDirectionsResponse>(responseStream);
                }

                if (getRouteDirectionsResponse.Errors != null && getRouteDirectionsResponse.Errors.Any())
                {
                    throw new Exception(getRouteDirectionsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getRouteDirectionsResponse.Directions;
                }
            }
        }

        internal static NameValueCollection CreateGetRouteDirectionsQueryString(string routeId)
        {
            var queryString = CreateQueryStringCollection();

            queryString.Add("rt", routeId);

            return queryString;
        }
    }
}
