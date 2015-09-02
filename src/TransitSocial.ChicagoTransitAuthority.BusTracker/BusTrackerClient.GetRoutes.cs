using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public partial class BusTrackerClient
    {
        public IEnumerable<Route> GetRoutes()
        {
            var request = this.CreateRequest("/bustime/api/v1/getroutes");

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetRoutesResponse getRoutesResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getRoutesResponse = this.Serializer.Deserialize<GetRoutesResponse>(responseStream);
                }

                if (getRoutesResponse.Errors != null && getRoutesResponse.Errors.Any())
                {
                    throw new Exception(getRoutesResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getRoutesResponse.Routes;
                }
            }
        }

        public Task<IEnumerable<Route>> GetRoutesAsync()
        {
            return this.GetRoutesAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync(CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/getroutes");

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetRoutesResponse getRoutesResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getRoutesResponse = this.Serializer.Deserialize<GetRoutesResponse>(responseStream);
                }

                if (getRoutesResponse.Errors != null && getRoutesResponse.Errors.Any())
                {
                    throw new Exception(getRoutesResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getRoutesResponse.Routes;
                }
            }
        }
    }
}
