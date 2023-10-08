using OBiletCase.Model.JourneyOffer;

namespace OBiletCase.UI.Models
{
    public class ExpeditionsViewModel
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public List<GetJourneysResultDatum>? Data { get; set; }
    }
}
