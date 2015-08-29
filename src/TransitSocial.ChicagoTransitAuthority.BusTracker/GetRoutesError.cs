using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class GetRoutesError
    {
        [XmlElement("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message { get; set; }
    }
}