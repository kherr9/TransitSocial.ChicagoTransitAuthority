using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        /// <summary>
        /// Use the gettime request to retrieve the current system date and time. Since BusTime is a timedependent
        /// system, it is important to synchronize your application with BusTime's system date and time
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetTimeAsync(CancellationToken token);

        /// <summary>
        /// Use the getvehicles request to retrieve vehicle information (i.e., locations) of all or a subset of vehicles
        /// currently being tracked by BusTime.
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <param name="routeIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// The vid and rt parameters cannot be combined in one request. If both parameters are specified
        /// on a request to getvehicles, only the first parameter specified on the request will be processed.
        /// </remarks>
        IEnumerable<Vehicle> GetVehicles(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds);

        /// <summary>
        /// Use the getvehicles request to retrieve vehicle information (i.e., locations) of all or a subset of vehicles
        /// currently being tracked by BusTime.
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <param name="routeIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// The vid and rt parameters cannot be combined in one request. If both parameters are specified
        /// on a request to getvehicles, only the first parameter specified on the request will be processed.
        /// </remarks>
        Task<IEnumerable<Vehicle>> GetVehiclesAsync(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds);

        /// <summary>
        /// Use the getvehicles request to retrieve vehicle information (i.e., locations) of all or a subset of vehicles
        /// currently being tracked by BusTime.
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <param name="routeIds"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <remarks>
        /// The vid and rt parameters cannot be combined in one request. If both parameters are specified
        /// on a request to getvehicles, only the first parameter specified on the request will be processed.
        /// </remarks>
        Task<IEnumerable<Vehicle>> GetVehiclesAsync(IEnumerable<string> vehicleIds, IEnumerable<string> routeIds, CancellationToken token);

        /// <summary>
        /// Use the getroutes request to retrieve the set of routes serviced by the system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Route> GetRoutes();

        /// <summary>
        /// Use the getroutes request to retrieve the set of routes serviced by the system.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutesAsync();

        /// <summary>
        /// Use the getroutes request to retrieve the set of routes serviced by the system.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutesAsync(CancellationToken token);

        /// <summary>
        /// Use the getdirections request to retrieve the set of directions serviced by the specified route.
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        IEnumerable<string> GetRouteDirections(string routeId);

        /// <summary>
        /// Use the getdirections request to retrieve the set of directions serviced by the specified route.
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRouteDirectionsAsync(string routeId);

        /// <summary>
        /// Use the getdirections request to retrieve the set of directions serviced by the specified route.
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRouteDirectionsAsync(string routeId, CancellationToken token);

        /////// <summary>
        /////// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /////// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /////// a particular route (regardless of direction) cannot be requested.
        /////// </summary>
        /////// <param name="routeId"></param>
        /////// <param name="direction"></param>
        /////// <returns></returns>
        ////IEnumerable<Stop> GetStops(string routeId, string direction);

        /////// <summary>
        /////// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /////// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /////// a particular route (regardless of direction) cannot be requested.
        /////// </summary>
        /////// <param name="routeId"></param>
        /////// <param name="direction"></param>
        /////// <returns></returns>
        ////Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction);

        /////// <summary>
        /////// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /////// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /////// a particular route (regardless of direction) cannot be requested.
        /////// </summary>
        /////// <param name="routeId"></param>
        /////// <param name="direction"></param>
        /////// <param name="token"></param>
        /////// <returns></returns>
        ////Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction, CancellationToken token);
    }
}
