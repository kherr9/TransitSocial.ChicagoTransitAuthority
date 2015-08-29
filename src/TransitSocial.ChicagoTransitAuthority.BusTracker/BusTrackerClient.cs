using System;
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
        }

        public string GetTime()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTimeAsync()
        {
            return this.GetTimeAsync(CancellationToken.None);
        }

        public Task<string> GetTimeAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}