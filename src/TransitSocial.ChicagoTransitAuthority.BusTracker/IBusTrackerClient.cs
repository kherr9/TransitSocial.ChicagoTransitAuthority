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

        /// <summary>
        /// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /// a particular route (regardless of direction) cannot be requested.
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IEnumerable<Stop> GetStops(string routeId, string direction);

        /// <summary>
        /// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /// a particular route (regardless of direction) cannot be requested.
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction);

        /// <summary>
        /// Use the getstops request to retrieve the set of stops for the specified route and direction.
        /// Stop lists are only available for a valid route/direction pair. In other words, a list of all stops that service
        /// a particular route (regardless of direction) cannot be requested.
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="direction"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Stop>> GetStopsAsync(string routeId, string direction, CancellationToken token);

        /// <summary>
        /// Use the getpredictions request to retrieve predictions for one or more stops or one or more vehicles.
        /// Predictions are always returned in ascending order according to prdtm.
        /// Use the vid parameter to retrieve predictions for one or more vehicles currently being tracked. A
        /// maximum of 10 vehicles can be specified.
        /// Use the stpid parameter to retrieve predictions for one or more stops. A maximum of 10 stops can be
        /// specified.
        /// Note: The vid and stpid parameters cannot be combined in one request. If both parameters are
        /// specified on a request to getpredictions, only the first parameter specified on the request will be
        /// processed.
        /// All call to getpredictions without specifying the vid or stpid parameter is not allowed.
        /// Use the top parameter to specify the maximum number of predictions to return. If top is not specified,
        /// then all predictions matching the specified parameters will be returned.
        /// </summary>
        /// <param name="stopIds"></param>
        /// <param name="routeIds"></param>
        /// <param name="vehicleIds"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IEnumerable<Prediction> GetPredictions(IEnumerable<int> stopIds, IEnumerable<string> routeIds, IEnumerable<string> vehicleIds, int? top);

        /// <summary>
        /// Use the getpredictions request to retrieve predictions for one or more stops or one or more vehicles.
        /// Predictions are always returned in ascending order according to prdtm.
        /// Use the vid parameter to retrieve predictions for one or more vehicles currently being tracked. A
        /// maximum of 10 vehicles can be specified.
        /// Use the stpid parameter to retrieve predictions for one or more stops. A maximum of 10 stops can be
        /// specified.
        /// Note: The vid and stpid parameters cannot be combined in one request. If both parameters are
        /// specified on a request to getpredictions, only the first parameter specified on the request will be
        /// processed.
        /// All call to getpredictions without specifying the vid or stpid parameter is not allowed.
        /// Use the top parameter to specify the maximum number of predictions to return. If top is not specified,
        /// then all predictions matching the specified parameters will be returned.
        /// </summary>
        /// <param name="stopIds"></param>
        /// <param name="routeIds"></param>
        /// <param name="vehicleIds"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        Task<IEnumerable<Prediction>> GetPredictionsAsync(IEnumerable<int> stopIds, IEnumerable<string> routeIds, IEnumerable<string> vehicleIds, int? top);

        /// <summary>
        /// Use the getpredictions request to retrieve predictions for one or more stops or one or more vehicles.
        /// Predictions are always returned in ascending order according to prdtm.
        /// Use the vid parameter to retrieve predictions for one or more vehicles currently being tracked. A
        /// maximum of 10 vehicles can be specified.
        /// Use the stpid parameter to retrieve predictions for one or more stops. A maximum of 10 stops can be
        /// specified.
        /// Note: The vid and stpid parameters cannot be combined in one request. If both parameters are
        /// specified on a request to getpredictions, only the first parameter specified on the request will be
        /// processed.
        /// All call to getpredictions without specifying the vid or stpid parameter is not allowed.
        /// Use the top parameter to specify the maximum number of predictions to return. If top is not specified,
        /// then all predictions matching the specified parameters will be returned.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="stopIds"></param>
        /// <param name="routeIds"></param>
        /// <param name="vehicleIds"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        Task<IEnumerable<Prediction>> GetPredictionsAsync(IEnumerable<int> stopIds, IEnumerable<string> routeIds, IEnumerable<string> vehicleIds, int? top, CancellationToken token);

        /// <summary>
        /// Use the getservicebulletins for a list of service bulletins that are in effect for a route(s) (rt), route &
        /// direction (rt & rtdir), route & direction & stop (rt & rtdir & stpid), or stop(s) (stpid). At a minimum, the
        /// rt or stpid parameter must be specified.
        /// A service bulletin (sb) definition without a srvc element indicates a "system-wide" service bulletin.
        /// System-wide service bulletins are valid for all routes/stops in the system.
        /// </summary>
        /// <param name="routeIds"></param>
        /// <param name="routeDirection"></param>
        /// <param name="stopIds"></param>
        /// <returns></returns>
        IEnumerable<ServiceBulletin> GetServiceBulletins(IEnumerable<string> routeIds, string routeDirection, IEnumerable<int> stopIds);

        /// <summary>
        /// Use the getservicebulletins for a list of service bulletins that are in effect for a route(s) (rt), route &
        /// direction (rt & rtdir), route & direction & stop (rt & rtdir & stpid), or stop(s) (stpid). At a minimum, the
        /// rt or stpid parameter must be specified.
        /// A service bulletin (sb) definition without a srvc element indicates a "system-wide" service bulletin.
        /// System-wide service bulletins are valid for all routes/stops in the system.
        /// </summary>
        /// <param name="routeIds"></param>
        /// <param name="routeDirection"></param>
        /// <param name="stopIds"></param>
        /// <returns></returns>
        Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<int> stopIds);

        /// <summary>
        /// Use the getservicebulletins for a list of service bulletins that are in effect for a route(s) (rt), route &
        /// direction (rt & rtdir), route & direction & stop (rt & rtdir & stpid), or stop(s) (stpid). At a minimum, the
        /// rt or stpid parameter must be specified.
        /// A service bulletin (sb) definition without a srvc element indicates a "system-wide" service bulletin.
        /// System-wide service bulletins are valid for all routes/stops in the system.
        /// </summary>
        /// <param name="routeIds"></param>
        /// <param name="routeDirection"></param>
        /// <param name="stopIds"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<int> stopIds, CancellationToken token);
    }
}
