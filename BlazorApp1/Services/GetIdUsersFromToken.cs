using BlazorApp1.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BlazorApp1.Services
{
    public class GetIdUsersFromToken : IGetUserIdAsync
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Token _getToken;

        public GetIdUsersFromToken(IHttpClientFactory clientFactory, Token getToken)
        {
            _clientFactory = clientFactory;
            _getToken = getToken;
        }

        public async Task<int?> GetUserIdAsync()
        {
            var client = _clientFactory.CreateClient(".NetApi");
            var requestGetId = new HttpRequestMessage(HttpMethod.Get, "api/Users/getId");
            requestGetId.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _getToken.GetToken());
            var responseId = await client.SendAsync(requestGetId);

            if (!responseId.IsSuccessStatusCode) 
                return null;

            var jsonString = await responseId.Content.ReadAsStringAsync();
            var id = JToken.Parse(jsonString);
            return (int)id;
        }
    }
}
