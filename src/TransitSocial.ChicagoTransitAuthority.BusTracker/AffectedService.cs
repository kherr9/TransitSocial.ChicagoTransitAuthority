using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Each srvc element represents one or a
    /// combination of route, direction and stop for which this service bulletin is
    /// valid. If the srvc element is not present, the service bulletin affects all
    /// routes and stops.
    /// </summary>
    [Serializable]
    public class AffectedService
    {
        [XmlElement("rt")]
        public string RouteId { get; set; }

        [XmlElement("rtdir")]
        public string RouteDirection { get; set; }

        [XmlElement("stpid")]
        public int? StopId { get; set; }

        [XmlElement("stpnm")]
        public string StopName { get; set; }
    }
}