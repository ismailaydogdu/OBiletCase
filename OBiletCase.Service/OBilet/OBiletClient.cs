using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OBiletCase.Model.JourneyOffer;
using OBiletCase.Service.Common.Extensions;
using OBiletCase.Service.Interfaces;
using System.Text;

namespace OBiletCase.Service.OBilet
{
    public class OBiletClient : IJourneyOfferClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _apiClientToken;
        private readonly string _baseUrl;
        public OBiletClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _apiClientToken = configuration["OBilet:ApiClientToken"];
            _baseUrl = configuration["OBiletServiceEndpoints:BaseUrl"];
        }
        public async Task<GetBusLocationsResult> GetBusLocations(string sessionId, string deviceId, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            var url = _configuration["OBiletServiceEndpoints:GetBusLocations"];

            GetBusLocationsRequest request = new()
            {
                Date = DateTime.Now,
                DeviceSession = new()
                {
                    DeviceId = deviceId,
                    SessionId = sessionId
                },
            };

            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }
            return await response.ReadJsonToAsync<GetBusLocationsResult>(cancellationToken);
        }

        public async Task<GetJourneysResult> GetJourneys(int originId, int destinationId, DateTime departureDate, string sessionId, string deviceId, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            var url = _configuration.GetConnectionString("OBiletServiceEndpoints:GetBusJourneys");
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadJsonToAsync<GetJourneysResult>(cancellationToken);
        }

        public async Task<GetSessionResult> GetSession(string clientIp, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            client.BaseAddress = new Uri(_baseUrl);
            GetSessionRequest request = new GetSessionRequest()
            {
                Type = 2,
                Connection = new Connection() { IpAddress = clientIp },
                Application = new Application()
                {
                    EquipmentId = "distribusion",
                    Version = " 1.0.0.0"
                }
            };

            var url = _configuration["OBiletServiceEndpoints:GetSession"];
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadJsonToAsync<GetSessionResult>(cancellationToken);
        }
    }
}
