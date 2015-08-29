using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class Vehicle
    {
        [XmlElement("vid", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VehicleId { get; set; }

        [XmlElement("tmstmp", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TimeStamp { get; set; }

        [XmlElement("lat", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double Latitude { get; set; }

        [XmlElement("lon", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double Longitude { get; set; }

        [XmlElement("hdg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int Heading { get; set; }

        [XmlElement("pid", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int PatternId { get; set; }

        [XmlElement("pdist", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int PatternDistance { get; set; }

        [XmlElement("rt", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Route { get; set; }

        [XmlElement("des", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Destination { get; set; }

        [XmlElement("dly", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool IsDelayed { get; set; }
    }
}