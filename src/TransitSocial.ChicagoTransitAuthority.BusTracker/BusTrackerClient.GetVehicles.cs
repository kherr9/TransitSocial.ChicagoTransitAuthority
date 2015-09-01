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
        public IEnumerable<Vehicle> GetVehicles(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds)
        {
            var request = this.CreateRequest("/bustime/api/v1/getvehicles", this.CreateGetVehiclesQueryString(vehicleIds, routeIds));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                GetVehiclesResponse getVehiclesResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getVehiclesResponse = this.Serializer.Deserialize<GetVehiclesResponse>(responseStream);
                }

                if (getVehiclesResponse.Errors != null && getVehiclesResponse.Errors.Any())
                {
                    throw new Exception(getVehiclesResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getVehiclesResponse.Vehicles;
                }
            }
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesAsync(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds)
        {
            return this.GetVehiclesAsync(vehicleIds, routeIds, CancellationToken.None);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds, CancellationToken token)
        {
            var request = this.CreateRequest("/bustime/api/v1/getvehicles", this.CreateGetVehiclesQueryString(vehicleIds, routeIds));

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = await request.GetResponseAsync(token))
            {
                GetVehiclesResponse getVehiclesResponse = null;
                using (var responseStream = response.GetResponseStream())
                {
                    getVehiclesResponse = this.Serializer.Deserialize<GetVehiclesResponse>(responseStream);
                }

                if (getVehiclesResponse.Errors != null && getVehiclesResponse.Errors.Any())
                {
                    throw new Exception(getVehiclesResponse.Errors.Select(x => x.Message).First());
                }
                else
                {
                    return getVehiclesResponse.Vehicles;
                }
            }
        }

        internal string CreateGetVehiclesQueryString(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds)
        {
            var temp = string.Empty;

            if (vehicleIds != null)
            {
                temp += string.Format("&{0}={1}", "Vid", string.Join(",", vehicleIds));
            }

            if (routeIds != null)
            {
                temp += string.Format("&{0}={1}", "Rt", string.Join(",", routeIds));
            }

            return temp;
        }
    }
}
