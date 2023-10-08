using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OBiletCase.Model.JourneyOffer;
using OBiletCase.Service.Common.Extensions;
using OBiletCase.Service.Interfaces;
using System.Collections;
using System.Text;

namespace OBiletCase.Service.OBilet
{
    public class OBiletClient : IJourneyOfferClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _apiClientToken;
        private readonly string _baseUrl;
        public OBiletClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _apiClientToken = configuration["OBilet:ApiClientToken"]; //User Secret fileden gelir 
            _baseUrl = configuration["OBiletServiceEndpoints:BaseUrl"]; // Appsettings OBilet Base url
        }


        public async Task<GetBusLocationsResult> GetBusLocations(string searchText, CancellationToken cancellationToken)
        {
            string deviceId = string.Empty;
            string sessionId = string.Empty;
            var deviveIdCheck = _httpContextAccessor.HttpContext.Session.TryGetValue("deviceId", out byte[] deviceIdByte);
            var sessionIdCheck = _httpContextAccessor.HttpContext.Session.TryGetValue("deviceId", out byte[] sessionIdByte);
            if (deviveIdCheck && sessionIdCheck)
            {
                deviceId = System.Text.Encoding.UTF8.GetString(deviceIdByte);
                sessionId = System.Text.Encoding.UTF8.GetString(sessionIdByte);
            }
            else
            {
                var sessionData = await GetSession(cancellationToken);
                if (sessionData != null)
                {
                    deviceId = sessionData.Data?.DeviceId!;
                    sessionId = sessionData.Data?.SessionId!;
                }
                else
                {
                    return null;
                }
            }
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            var url = _configuration["OBiletServiceEndpoints:GetBusLocations"];

            GetBusLocationsRequest request = new()
            {
                Data = string.IsNullOrEmpty(searchText) ? null : searchText,
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
            var data = await response.ReadJsonToAsync<GetBusLocationsResult>(cancellationToken);
            if (data.Status == "DeviceSessionError")
            {
                var sessionData = await GetSession(cancellationToken);
                if (sessionData != null)
                {
                    return await GetBusLocations(searchText, cancellationToken);
                }
                return null;
            }
            return data;
        }

        public async Task<GetJourneysResult> GetJourneys(int originId, int destinationId, DateTime departureDate, CancellationToken cancellationToken)
        {
            string deviceId = string.Empty;
            string sessionId = string.Empty;
            var deviveIdCheck = _httpContextAccessor.HttpContext.Session.TryGetValue("deviceId", out byte[] deviceIdByte);
            var sessionIdCheck = _httpContextAccessor.HttpContext.Session.TryGetValue("deviceId", out byte[] sessionIdByte);
            if (deviveIdCheck && sessionIdCheck)
            {
                deviceId = System.Text.Encoding.UTF8.GetString(deviceIdByte);
                sessionId = System.Text.Encoding.UTF8.GetString(sessionIdByte);
            }
            else
            {
                var sessionData = await GetSession(cancellationToken);
                if (sessionData != null)
                {
                    deviceId = sessionData.Data?.DeviceId!;
                    sessionId = sessionData.Data?.SessionId!;
                }
                else
                {
                    return null;
                }
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            var url = _configuration["OBiletServiceEndpoints:GetBusJourneys"];
            client.BaseAddress = new Uri(_baseUrl);
            GetJourneysRequest request = new()
            {
                DeviceSession = new()
                {
                    DeviceId = deviceId,
                    SessionId = sessionId
                },
                Date = DateTime.Now,
                Language = "tr-TR",
                Data = new()
                {
                    DepartureDate = departureDate.ToString("yyyy-MM-dd"),
                    DestinationId = destinationId,
                    OriginId = originId
                }
            };
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadJsonToAsync<GetJourneysResult>(cancellationToken);
        }

        private async Task<GetSessionResult> GetSession(CancellationToken cancellationToken)
        {
#if DEBUG
            var clientIp = "159.146.43.181"; //Localde alınan ipde sıkıntı olduğu için debug modda manuel olarak eklemek için bu şekilde bırakılmıştır
#else
            var clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
#endif
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _apiClientToken);
            client.BaseAddress = new Uri(_baseUrl);
            GetSessionRequest request = new GetSessionRequest()
            {
                Type = 1,
                Connection = new Connection() { IpAddress = clientIp },
                Application = new Application()
                {
                    EquipmentId = "distribusion",
                    Version = " 1.0.0.0"
                }
            };

            var url = _configuration["OBiletServiceEndpoints:GetSession"];
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), cancellationToken);
            if (!response.IsSuccessStatusCode) //429 hatası sonucu default veri kullanımı için
            {
                _httpContextAccessor.HttpContext.Session.Set("sessionId", Encoding.ASCII.GetBytes("PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA="));
                _httpContextAccessor.HttpContext.Session.Set("deviceId", Encoding.ASCII.GetBytes("PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA="));
                return new GetSessionResult() { Data = new Data() { SessionId = "PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA=", DeviceId = "PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA=" } };
            }
            var data = await response.ReadJsonToAsync<GetSessionResult>(cancellationToken);
#if DEBUG //429 hatası sonucu default veri kullanımı için
            _httpContextAccessor.HttpContext.Session.Set("sessionId", Encoding.ASCII.GetBytes("PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA="));
            _httpContextAccessor.HttpContext.Session.Set("deviceId", Encoding.ASCII.GetBytes("PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA="));
#else
            _httpContextAccessor.HttpContext.Session.Set("deviceId", Encoding.ASCII.GetBytes(data.Data?.DeviceId!));
            _httpContextAccessor.HttpContext.Session.Set("sessionId", Encoding.ASCII.GetBytes(data.Data?.SessionId!));

#endif
            return data;
        }
    }
}
