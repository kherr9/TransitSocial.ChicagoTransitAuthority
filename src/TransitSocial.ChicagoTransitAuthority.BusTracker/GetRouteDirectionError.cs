using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetRouteDirectionError
    {
        [XmlElement("msg")]
        public string Message { get; set; }

        [XmlElement("rt")]
        public string RouteId { get; set; }
    }
}