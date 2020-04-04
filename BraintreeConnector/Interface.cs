namespace BraintreeGraphQLConnector.Interface
{
    public interface IAppSettings
    {
        string Url { get; set; }
        string PrivateKey { set; get; }
        string PublicKey { get; set; }
        string Version { get; set; }
    }
}