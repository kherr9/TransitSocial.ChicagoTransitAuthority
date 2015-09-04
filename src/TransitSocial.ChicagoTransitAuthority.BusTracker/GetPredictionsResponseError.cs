using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetPredictionsResponseError
    {
        [XmlElement("stpid")]
        public int StopId { get; set; }

        [XmlElement("vid")]
        public string VehicleId { get; set; }

        [XmlElement("msg")]
        public string Message { get; set; }
    }
}