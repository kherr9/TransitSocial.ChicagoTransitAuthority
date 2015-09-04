using System;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    [Serializable]
    public class Prediction
    {
        /// <summary>
        /// Date and time (local) the prediction was
        /// generated. Date and time is represented in the following format:
        /// YYYYMMDD HH:MM. Month is represented as two digits where January
        /// is equal to “01” and December is equal to “12”. Time is represented using
        /// a 24-hour clock.
        /// </summary>
        [XmlElement("tmstmp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Type of prediction. 'A' for an arrival
        /// prediction (prediction of when the vehicle will arrive at this stop). 'D' for a
        /// departure prediction (prediction of when the vehicle will depart this stop, if
        /// applicable). Predictions made for first stops of a route or layovers are
        /// examples of departure predictions.
        /// </summary>
        [XmlElement("typ")]
        public string Type { get; set; }

        [XmlElement("stpid")]
        public int StopId { get; set; }

        [XmlElement("stpnm")]
        public string StopName { get; set; }

        [XmlElement("vid")]
        public int VehicleId { get; set; }

        /// <summary>
        /// Linear distance (feet) left to be traveled
        /// by the vehicle before it reaches the stop associated with this prediction.
        /// </summary>
        [XmlElement("dstp")]
        public int Distance { get; set; }

        [XmlElement("rt")]
        public string RouteId { get; set; }

        [XmlElement("rtdir")]
        public string RouteDirection { get; set; }

        /// <summary>
        /// Final destination of the vehicle
        /// associated with this prediction.
        /// </summary>
        [XmlElement("des")]
        public string Destination { get; set; }

        /// <summary>
        /// Predicted date and time (local) of a
        /// vehicle's arrival or departure to the stop associated with this prediction.
        /// Date and time is represented in the following format: YYYYMMDD
        /// HH:MM. Month is represented as two digits where January is equal to
        /// "01" and December is equal to “12”. Time is represented using a 24-hour
        /// clock.
        /// </summary>
        [XmlElement("prdtm")]
        public string PredictionTime { get; set; }

        /// <summary>
        /// "true" if the vehicle is delayed. The dly
        /// element is only present if the vehicle that generated this prediction is
        /// delayed.
        /// </summary>
        [XmlElement("")]
        public bool IsDelayed { get; set; }
    }
}