using System.Net;
using System.Net.Http;
using WebAdvert.Web.Models;

namespace WebAdvert.Web.ServiceClient
{
    public class SearchApiClient : ISearchApiClient
    {
        private readonly HttpClient _client;
        private readonly string BaseAddress = string.Empty;
        public SearchApiClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            BaseAddress = configuration.GetSection("SearchApi").GetValue<string>("url");
        }

        public async Task<List<AdvertType>> Search(string keyword)
        {
            var result = new List<AdvertType>();
            var callUrl = $"{BaseAddress}/search/v1/{keyword}";
            var httpResponse = await _client.GetAsync(new Uri(callUrl));

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var allAdverts = await httpResponse.Content.ReadFromJsonAsync<List<AdvertType>>();
                if(allAdverts != null)
                    result.AddRange(allAdverts);
            }

            return result;
        }
    }
}
