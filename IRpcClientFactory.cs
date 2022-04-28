    public interface IRpcClientFactory
    {
        Uri GetUri();
        HttpClient GetClient();
    }