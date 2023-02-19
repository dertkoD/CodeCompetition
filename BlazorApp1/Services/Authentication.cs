using System.Net.Http.Headers;

namespace BlazorApp1.Services
{
    public class Authentication
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly Token _getToken;

        public Authentication(IHttpClientFactory clientFactory, Token getToken)
        {
            _clientFactory = clientFactory;
            _getToken = getToken;
        }

        public HttpClient Authenications (string baseUrl)
        {         
           var client = _clientFactory.CreateClient(baseUrl);
            client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", _getToken.GetToken());

            return client;
        }
    }
}
