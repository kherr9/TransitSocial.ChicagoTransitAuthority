using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetStopsResponseError
    {
        [XmlElement("rt")]
        public string RouteId { get; set; }

        [XmlElement("dir")]
        public string Direction { get; set; }

        [XmlElement("msg")]
        public string Message { get; set; }
    }
}