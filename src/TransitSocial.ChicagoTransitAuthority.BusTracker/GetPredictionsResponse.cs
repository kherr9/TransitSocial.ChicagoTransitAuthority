using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    /// <summary>
    /// Response envelope for GetPredictions
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "bustime-response")]
    public class GetPredictionsResponse
    {
        [XmlElement("error")]
        public GetPredictionsResponseError[] Errors { get; set; }

        [XmlElement("prd")]
        public Prediction[] Predictions { get; set; }
    }
}
