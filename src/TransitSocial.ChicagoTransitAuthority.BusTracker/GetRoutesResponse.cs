using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Response envelope for GetRoutes
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetRoutesResponse
    {
        [XmlElement("error")]
        public GetRoutesError[] Errors { get; set; }

        [XmlElement("route")]
        public Route[] Routes { get; set; }
    }
}
