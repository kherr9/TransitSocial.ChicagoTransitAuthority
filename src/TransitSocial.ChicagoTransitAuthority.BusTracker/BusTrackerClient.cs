using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public class BusTrackerClient : IBusTrackerClient
    {
        private readonly string urlBase;

        private readonly string key;

        public BusTrackerClient(string urlBase, string key)
        {
            if (urlBase == null)
            {
                throw new ArgumentNullException("urlBase");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            this.urlBase = urlBase;
            this.key = key;
            this.Serializer = new Serializer();
        }

        public ISerializer Serializer { get; set; }

        public string GetTime()
        {
            var request = this.CreateRequest("/bustime/api/v1/gettime");

            request.Method = "GET";

            // will throw WebException if not success status
            using (var response = (HttpWebResponse)request.GetResponse())
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

        public Task<string> GetTimeAsync()
        {
            return this.GetTimeAsync(CancellationToken.None);
        }

        public Task<string> GetTimeAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        private HttpWebRequest CreateRequest(string relativePath)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }

            var uri = this.urlBase + relativePath + "?key=" + this.key;

            var request = WebRequest.CreateHttp(uri);

            return request;
        }
    }
}