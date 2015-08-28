using System;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <remarks/>
    [Serializable]
    public class Error
    {
        [System.Xml.Serialization.XmlElementAttribute("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message { get; set; }
    }
}