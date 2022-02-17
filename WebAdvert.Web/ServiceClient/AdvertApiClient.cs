using AdvertApi.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace WebAdvert.Web.ServiceClient
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public AdvertApiClient(IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.mapper = mapper;

            var createUrl = configuration.GetSection("AdvertApi").GetValue<string>("CreateUrl");
            httpClient.BaseAddress = new Uri(createUrl);
            httpClient.DefaultRequestHeaders.Add("content-type", "application/json");
        }

        public async Task<bool> ConfirmAsync(ConfirmAdvertRequest model)
        {
            var advertModel = mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await httpClient.PutAsync(new Uri($"{httpClient.BaseAddress}/Confirm"), new StringContent(jsonModel));

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        // Send a request to the web api
        public async Task<AdvertResponse> CreateAsync(CreateAdvertModel model)
        {
            var advertApiModel = mapper.Map<AdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await httpClient.PostAsync(httpClient.BaseAddress, new StringContent(jsonModel));

            var responseJson = await response.Content.ReadAsStringAsync();
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);
            var advertResponse = mapper.Map<AdvertResponse>(createAdvertResponse);

            return advertResponse;
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            var apiCallResponse = await httpClient.GetAsync(new Uri($"{httpClient.BaseAddress}/all"));
            var allAdvertModels = await apiCallResponse.Content.ReadFromJsonAsync<List<Advertisement>>();

            if(allAdvertModels != null)
                return allAdvertModels.Select(x => mapper.Map<Advertisement>(x)).ToList();
            else
                return new List<Advertisement>();
        }
    }
}
