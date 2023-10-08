using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletCase.Model.JourneyOffer
{

    public class GetJourneysRequestData
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public string DepartureDate { get; set; } = null!;
    }


    public class GetJourneysRequest
    {
        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; } = null!;

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("data")]
        public GetJourneysRequestData Data { get; set; } = null!;
    }
}

