using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public partial class BusTrackerClient
    {
        public IEnumerable<ServiceBulletin> GetServiceBulletins(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds)
        {
            return this.GetServiceBulletinsAsync(routeIds, routeDirection, stopIds, CancellationToken.None);
        }

        public Task<IEnumerable<ServiceBulletin>> GetServiceBulletinsAsync(IEnumerable<string> routeIds, string routeDirection, IEnumerable<string> stopIds, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
