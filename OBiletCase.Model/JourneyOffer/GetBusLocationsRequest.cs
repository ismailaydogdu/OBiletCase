using Newtonsoft.Json;

namespace OBiletCase.Model.JourneyOffer
{
    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; } = null!;

        [JsonProperty("device-id")]
        public string DeviceId { get; set; } = null!;
    }

    public class GetBusLocationsRequest
    {
        [JsonProperty("data")]
        public string? Data { get; set; }

        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; } = null!;

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "en-EN";
    }


}
