using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetServiceBulletinResponseError
    {
        [XmlElement("rt")]
        public string RouteId { get; set; }

        [XmlElement("rtdir")]
        public string RouteDirection { get; set; }

        [XmlElement("stpid")]
        public int? StopId { get; set; }

        [XmlElement("msg")]
        public string Message { get; set; }
    }
}