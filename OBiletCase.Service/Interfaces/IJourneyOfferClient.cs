

using OBiletCase.Model.JourneyOffer;

namespace OBiletCase.Service.Interfaces
{
    public interface IJourneyOfferClient
    {

        Task<GetBusLocationsResult> GetBusLocations(string term, CancellationToken cancellationToken);
        Task<GetJourneysResult> GetJourneys(int originId, int destinationId, DateTime departureDate, CancellationToken cancellationToken);
    }
}
