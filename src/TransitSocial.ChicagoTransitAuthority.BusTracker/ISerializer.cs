using System.IO;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public interface ISerializer
    {
        TModel Deserialize<TModel>(string input);

        TModel Deserialize<TModel>(Stream source);
    }
}
