using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Response envelope for GetTime
    /// </summary>
    [Serializable]
    [XmlTypeAttribute(TypeName = "bustime-response")]
    public class GetTimeResponse
    {
        [XmlElement("error")]
        public GetTimeError[] Errors { get; set; }

        /// <summary>
        /// Child element of the root element containing the current system date and
        /// (local) time. Date and time is represented in the following format:
        /// YYYYMMDD HH:MM:SS. Month is represented as two digits where
        /// January is equal to “01” and December is equal to “12”. Time is
        /// represented using a 24-hour clock.
        /// </summary>
        [XmlElementAttribute("tm")]
        public string Time { get; set; }
    }
}
