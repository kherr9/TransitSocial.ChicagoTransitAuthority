using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class Stop
    {
        [XmlElement("stpid")]
        public int StopId { get; set; }

        [XmlElement("stpnm")]
        public string Name { get; set; }

        [XmlElement("lat")]
        public double Latitude { get; set; }

        [XmlElement("lon")]
        public double Longitude { get; set; }
    }
}