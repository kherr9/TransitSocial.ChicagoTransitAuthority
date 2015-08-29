using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetVehiclesError
    {
        [XmlElement("vid", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VehicleId { get; set; }

        [XmlElement("rt", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Route { get; set; }

        [XmlElement("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message { get; set; }
    }
}