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
        public Task<string> GetTimeAsync()
        {
            return this.GetTimeAsync(CancellationToken.None);
        }

        public async Task<string> GetTimeAsync(CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/gettime");

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetTimeResponse getTimeResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getTimeResponse = this.Serializer.Deserialize<GetTimeResponse>(responseStream);
                }

                if (getTimeResponse.Errors != null && getTimeResponse.Errors.Any())
                {
                    throw new Exception(getTimeResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getTimeResponse.Time;
                }
            }
        }
    }
}