using System.Net.Http.Headers;
using System.Text;
    public class StaticRestClientFactory : IRpcClientFactory
    {
        private static readonly Lazy<HttpClient> ClientInstance = new Lazy<HttpClient>(
            () =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(AppSettings.rpcuri)
                };
                var auth = AppSettings.rpcuser + ":" + AppSettings.rpcpassword;
                auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth), Base64FormattingOptions.None);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                return client;
            });


        public Uri GetUri()
        {
            return new Uri(AppSettings.rpcuri);
        }

        public HttpClient GetClient()
        {
            return ClientInstance.Value;
        }
    }
 