namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "bustime-response")]
    [System.Xml.Serialization.XmlRootAttribute("bustime-response", Namespace = "", IsNullable = false)]
    public class BusTimeResponse
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("error", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Error[] Error { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tm")]
        public string Time { get; set; }
    }
}
