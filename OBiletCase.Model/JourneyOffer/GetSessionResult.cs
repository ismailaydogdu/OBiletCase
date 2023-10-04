using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletCase.Model.JourneyOffer
{
    public class GetSessionResult
    {
        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("data")]
        public Data? Data { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("user-message")]
        public string? UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public string? ApiRequestId { get; set; }

        [JsonProperty("controller")]
        public string? Controller { get; set; }
    }
    public class Data
    {
        [JsonProperty("session-id")]
        public string? SessionId { get; set; }

        [JsonProperty("device-id")]
        public string? DeviceId { get; set; }
    }


}
