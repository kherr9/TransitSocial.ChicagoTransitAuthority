using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [XmlTypeAttribute(TypeName = "bustime-response")]
    public class BusTimeResponse
    {
        /// <remarks/>
        [XmlElement("error")]
        public Error[] Error { get; set; }

        /// <remarks/>
        [XmlElementAttribute("tm")]
        public string Time { get; set; }
    }
}
