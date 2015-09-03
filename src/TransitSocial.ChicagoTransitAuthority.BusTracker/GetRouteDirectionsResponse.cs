using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Response envelope for GetRoutes
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetRouteDirectionsResponse
    {
        [XmlElement("error")]
        public GetRouteDirectionError[] Errors { get; set; }

        [XmlElement("dir")]
        public string[] Directions { get; set; }
    }
}
