namespace PropertyAgencyMobileApp.Services
{
    public interface ISurrogateIdResolver<TId>
    {
        TId ResolveId();
    }
}
