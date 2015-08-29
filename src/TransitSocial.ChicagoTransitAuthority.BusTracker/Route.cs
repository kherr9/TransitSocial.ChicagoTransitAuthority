using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class Route
    {
        [XmlElement("rt", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RouteId { get; set; }

        [XmlElement("rtnm", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
}