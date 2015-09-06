using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class ServiceBulletin
    {
        /// <summary>
        /// Unique name/identifier of the service bulletin.
        /// </summary>
        [XmlElement("nm")]
        public string Name { get; set; }

        /// <summary>
        /// A short title for this service bulletin.
        /// </summary>
        [XmlElement("sbj")]
        public string Subject { get; set; }

        /// <summary>
        ///  Full text of the service bulletin
        /// </summary>
        [XmlElement("dtl")]
        public string Detail { get; set; }

        /// <summary>
        ///  A short text alternative to the service bulletin detail.
        /// </summary>
        [XmlElement("brf")]
        public string Brief { get; set; }

        /// <summary>
        /// Service bulletin priority. The possible values are "High," "Medium," and "Low".
        /// </summary>
        [XmlElement("prty")]
        public string Priority { get; set; }

        /// <summary>
        /// Each srvc element represents one or a
        /// combination of route, direction and stop for which this service bulletin is
        /// valid. If the srvc element is not present, the service bulletin affects all
        /// routes and stops.
        /// </summary>
        [XmlElement("srvc")]
        public AffectedService[] AffectedServices { get; set; }
    }
}
