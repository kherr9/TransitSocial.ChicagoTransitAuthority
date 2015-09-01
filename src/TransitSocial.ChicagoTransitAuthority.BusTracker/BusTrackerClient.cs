using System;
using System.Collections.Specialized;
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

        private HttpWebRequest CreateRequest(string relativePath, NameValueCollection queryStringCollection = null)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }

            queryStringCollection = queryStringCollection ?? CreateQueryStringCollection();

            queryStringCollection.Add("key", this.key);

            var uri = this.urlBase + relativePath + "?" + queryStringCollection.ToString();

            var request = WebRequest.CreateHttp(uri);

            return request;
        }

        private static NameValueCollection CreateQueryStringCollection()
        {
            return System.Web.HttpUtility.ParseQueryString(string.Empty);
        }
    }
}