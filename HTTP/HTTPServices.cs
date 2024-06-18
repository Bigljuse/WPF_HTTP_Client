using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WPF_HTTP_Client.HTTP
{
    public partial class HTTPServices
    {
        private static ServiceCollection _http_Services = new ServiceCollection();

        static HTTPServices()
        {
            _http_Services.AddHttpClient();
        }

        public HttpClient CreateHttpClient(string clientName, Uri uri)
        {
            var provider = _http_Services.BuildServiceProvider();
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient(clientName);

            client.BaseAddress = uri;
            ConfigureClient(client);

            //var crypto = SHA512.Create();
            //var text = Encoding.ASCII.GetBytes("hello");
            //byte[] hash = crypto.ComputeHash(text);
            //string decoded = Convert.ToBase64String(hash);
            //Console.WriteLine(decoded);

            return client;
        }
    }

    public partial class HTTPServices
    {
        private void ConfigureClient(HttpClient client)
        {
            ConfigureAuthScheme(client);
            ConfigureAgentHeader(client);
        }

        private void ConfigureAuthScheme(HttpClient client)
        {
            var scheme = AuthenticationSchemes.Anonymous.ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme);
        }

        private void ConfigureAgentHeader(HttpClient client)
        {
            var infoApp = new ProductInfoHeaderValue("Client", "1.0");
            //var infoAppType = new ProductInfoHeaderValue("Comment");

            client.DefaultRequestHeaders.UserAgent.Add(infoApp);
            //client.DefaultRequestHeaders.UserAgent.Add(infoAppType);
        }
    }
}
