using System.IO;
using System.Reflection;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources
{
    public class ResourceRepository
    {
        private readonly Serializer serializer = new Serializer();

        public string GetString(string path)
        {
            using (var stream = this.GetStream(path))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public TModel GetAs<TModel>(string path)
        {
            var xml = this.GetString(path);

            return this.serializer.Deserialize<TModel>(xml);
        }

        private Stream GetStream(string path)
        {
            var fullPath = this.GetType().Namespace + "." + path;

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);

            if (stream == null)
            {
                throw new FileNotFoundException(string.Format("Unable to find resource '{0}'", fullPath));
            }

            return stream;
        }
    }
}
