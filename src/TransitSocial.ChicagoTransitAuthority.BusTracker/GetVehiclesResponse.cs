using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{    /// <summary>
    /// Response envelope for GetVehicles
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetVehiclesResponse
    {
        [XmlElement("error")]
        public GetVehiclesError[] Errors { get; set; }

        [XmlElement("vehicle")]
        public Vehicle[] Vehicles { get; set; }
    }
}
