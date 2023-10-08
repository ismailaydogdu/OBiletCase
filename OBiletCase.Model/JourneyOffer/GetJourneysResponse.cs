using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletCase.Model.JourneyOffer
{
   

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        

        public class Feature
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("priority")]
            public int? Priority { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public object Description { get; set; }

            [JsonProperty("is-promoted")]
            public bool IsPromoted { get; set; }

            [JsonProperty("back-color")]
            public object BackColor { get; set; }

            [JsonProperty("fore-color")]
            public object ForeColor { get; set; }
        }

        public class Journey
        {
            [JsonProperty("kind")]
            public string Kind { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("stops")]
            public List<Stop> Stops { get; set; }

            [JsonProperty("origin")]
            public string Origin { get; set; }

            [JsonProperty("destination")]
            public string Destination { get; set; }

            [JsonProperty("departure")]
            public DateTime Departure { get; set; }

            [JsonProperty("arrival")]
            public DateTime Arrival { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("original-price")]
            public double OriginalPrice { get; set; }

            [JsonProperty("internet-price")]
            public double InternetPrice { get; set; }

            [JsonProperty("provider-internet-price")]
            public double? ProviderInternetPrice { get; set; }

            [JsonProperty("booking")]
            public object Booking { get; set; }

            [JsonProperty("bus-name")]
            public string BusName { get; set; }

            [JsonProperty("policy")]
            public Policy Policy { get; set; }

            [JsonProperty("features")]
            public List<string> Features { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("available")]
            public object Available { get; set; }

            [JsonProperty("partner-provider-code")]
            public object PartnerProviderCode { get; set; }

            [JsonProperty("peron-no")]
            public object PeronNo { get; set; }

            [JsonProperty("partner-provider-id")]
            public object PartnerProviderId { get; set; }

            [JsonProperty("is-segmented")]
            public bool IsSegmented { get; set; }

            [JsonProperty("partner-name")]
            public object PartnerName { get; set; }

            [JsonProperty("provider-code")]
            public object ProviderCode { get; set; }
        }

        public class Policy
        {
            [JsonProperty("max-seats")]
            public object MaxSeats { get; set; }

            [JsonProperty("max-single")]
            public int? MaxSingle { get; set; }

            [JsonProperty("max-single-males")]
            public int? MaxSingleMales { get; set; }

            [JsonProperty("max-single-females")]
            public int MaxSingleFemales { get; set; }

            [JsonProperty("mixed-genders")]
            public bool MixedGenders { get; set; }

            [JsonProperty("gov-id")]
            public bool GovId { get; set; }

            [JsonProperty("lht")]
            public bool Lht { get; set; }
        }

        public class GetJourneysResponse
    {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("data")]
            public List<Datum> Data { get; set; }

            [JsonProperty("message")]
            public object Message { get; set; }

            [JsonProperty("user-message")]
            public object UserMessage { get; set; }

            [JsonProperty("api-request-id")]
            public object ApiRequestId { get; set; }

            [JsonProperty("controller")]
            public string Controller { get; set; }

            [JsonProperty("client-request-id")]
            public object ClientRequestId { get; set; }

            [JsonProperty("web-correlation-id")]
            public object WebCorrelationId { get; set; }

            [JsonProperty("correlation-id")]
            public string CorrelationId { get; set; }
        }

        public class Stop
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("kolayCarLocationId")]
            public int? KolayCarLocationId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("station")]
            public string Station { get; set; }

            [JsonProperty("time")]
            public DateTime Time { get; set; }

            [JsonProperty("is-origin")]
            public bool IsOrigin { get; set; }

            [JsonProperty("is-destination")]
            public bool IsDestination { get; set; }
        }


    }
