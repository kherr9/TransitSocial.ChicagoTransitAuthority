using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public interface IBusTrackerClient
    {
        /// <summary>
        /// Use the gettime request to retrieve the current system date and time. Since BusTime is a timedependent
        /// system, it is important to synchronize your application with BusTime's system date and time
        /// </summary>
        /// <returns></returns>
        string GetTime();

        /// <summary>
        /// Use the gettime request to retrieve the current system date and time. Since BusTime is a timedependent
        /// system, it is important to synchronize your application with BusTime's system date and time
        /// </summary>
        /// <returns></returns>
        Task<string> GetTimeAsync();
    }
}
