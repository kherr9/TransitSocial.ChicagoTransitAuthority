using System;
using System.Net;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public partial class BusTrackerClient : IBusTrackerClient
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

        private HttpWebRequest CreateRequest(string relativePath, string queryString = null)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }

            var uri = this.urlBase + relativePath + "?key=" + this.key;

            if (queryString != null)
            {
                uri += queryString;
            }

            var request = WebRequest.CreateHttp(uri);

            return request;
        }
    }
}