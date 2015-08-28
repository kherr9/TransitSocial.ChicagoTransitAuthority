namespace TransitSocial.ChicagoTransitAuthority.BusTracker
{
    public interface ISerializer
    {
        TModel Deserialize<TModel>(string input);
    }
}
