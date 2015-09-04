using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Response envelope for GetStops
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetStopsResponse
    {
        [XmlElement("error")]
        public GetStopsResponseError[] Errors { get; set; }

        [XmlElement("stop")]
        public Stop[] Stops { get; set; }
    }
}