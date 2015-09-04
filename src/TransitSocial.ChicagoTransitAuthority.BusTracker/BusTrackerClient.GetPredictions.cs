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
        public IEnumerable<Prediction> GetPredictions(
            IEnumerable<string> stopIds,
            IEnumerable<string> routeIds,
            IEnumerable<string> vehicleIds,
            int? top)
        {
            var request = this.CreateRequest("/bustime/api/v1/getpredictions", CreateGetPredictionsQueryString(stopIds, routeIds, vehicleIds, top));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetPredictionsResponse getPredictionsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getPredictionsResponse = this.Serializer.Deserialize<GetPredictionsResponse>(responseStream);
                }

                if (getPredictionsResponse.Errors != null && getPredictionsResponse.Errors.Any())
                {
                    throw new Exception(getPredictionsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getPredictionsResponse.Predictions;
                }
            }
        }

        public Task<IEnumerable<Prediction>> GetPredictionsAsync(
            IEnumerable<string> stopIds,
            IEnumerable<string> routeIds,
            IEnumerable<string> vehicleIds,
            int? top)
        {
            return this.GetPredictionsAsync(stopIds, routeIds, vehicleIds, top, CancellationToken.None);
        }

        public async Task<IEnumerable<Prediction>> GetPredictionsAsync(
            IEnumerable<string> stopIds,
            IEnumerable<string> routeIds,
            IEnumerable<string> vehicleIds,
            int? top,
            CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/getpredictions", CreateGetPredictionsQueryString(stopIds, routeIds, vehicleIds, top));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetPredictionsResponse getPredictionsResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getPredictionsResponse = this.Serializer.Deserialize<GetPredictionsResponse>(responseStream);
                }

                if (getPredictionsResponse.Errors != null && getPredictionsResponse.Errors.Any())
                {
                    throw new Exception(getPredictionsResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getPredictionsResponse.Predictions;
                }
            }
        }

        internal static NameValueCollection CreateGetPredictionsQueryString(IEnumerable<string> stopIds, IEnumerable<string> routeIds, IEnumerable<string> vehicleIds, int? top)
        {
            var queryString = CreateQueryStringCollection();

            if (stopIds != null)
            {
                var value = string.Join(",", stopIds);

                if (value != string.Empty)
                {
                    queryString.Add("stpid", value);
                }
            }

            if (routeIds != null)
            {
                var value = string.Join(",", routeIds);

                if (value != string.Empty)
                {
                    queryString.Add("rt", value);
                }
            }

            if (vehicleIds != null)
            {
                var value = string.Join(",", vehicleIds);

                if (value != string.Empty)
                {
                    queryString.Add("vid", value);
                }
            }

            if (top.HasValue)
            {
                queryString.Add("top", top.Value.ToString());
            }

            return queryString;
        }
    }
}
