using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public class BusTrackerClient : IBusTrackerClient
    {
        public string GetTime()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetTimeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}