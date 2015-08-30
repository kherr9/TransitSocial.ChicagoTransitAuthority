using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public class Serializer : ISerializer
    {
        public TModel Deserialize<TModel>(string input)
        {
            using (var reader = new StringReader(input))
            using (var xmlReader = XmlReader.Create(reader))
            {
                var ser = new XmlSerializer(typeof(TModel));
                return (TModel)ser.Deserialize(xmlReader);
            }
        }

        public TModel Deserialize<TModel>(Stream source)
        {
            using (var xmlReader = XmlReader.Create(source))
            {
                var ser = new XmlSerializer(typeof(TModel));
                return (TModel)ser.Deserialize(xmlReader);
            }
        }
    }
}