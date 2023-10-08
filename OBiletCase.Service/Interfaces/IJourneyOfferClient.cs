

using OBiletCase.Model.JourneyOffer;

namespace OBiletCase.Service.Interfaces
{
    public interface IJourneyOfferClient
    {

        Task<GetSessionResult> GetSession(CancellationToken cancellationToken);
        Task<GetBusLocationsResult> GetBusLocations(string term, CancellationToken cancellationToken);
        Task<GetJourneysResult> GetJourneys(int originId, int destinationId, DateTime departureDate, CancellationToken cancellationToken);
    }
}
