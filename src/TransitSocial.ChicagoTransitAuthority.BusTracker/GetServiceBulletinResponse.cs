using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetServiceBulletinResponse
    {
        [XmlElement("error")]
        public GetServiceBulletinResponseError[] Errors { get; set; }

        [XmlElement("sb")]
        public ServiceBulletin[] ServiceBulletins { get; set; }
    }
}