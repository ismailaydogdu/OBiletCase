using Newtonsoft.Json;

namespace OBiletCase.Model.JourneyOffer
{
    public class GetBusLocationsResult
    {
        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("data")]
        public List<Datum>? Data { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("user-message")]
        public string? UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public int ApiRequestId { get; set; }

        [JsonProperty("controller")]
        public string? Controller { get; set; }
    }
    public class Datum
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parent-id")]
        public int ParentId { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation? GeoLocation { get; set; }

        [JsonProperty("tz-code")]
        public string? TzCode { get; set; }

        [JsonProperty("weather-code")]
        public string WeatherCode { get; set; } = null!;

        [JsonProperty("rank")]
        public int? Rank { get; set; }

        [JsonProperty("reference-code")]
        public string? ReferenceCode { get; set; }

        [JsonProperty("keywords")]
        public string? Keywords { get; set; }
    }

    public class GeoLocation
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("zoom")]
        public int Zoom { get; set; }
    }

 
}
