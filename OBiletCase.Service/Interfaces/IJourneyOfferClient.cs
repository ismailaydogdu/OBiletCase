

using OBiletCase.Model.JourneyOffer;

namespace OBiletCase.Service.Interfaces
{
    public interface IJourneyOfferClient
    {
        Task<GetSessionResult> GetSession(string clientIp, CancellationToken cancellationToken);
        Task<GetBusLocationsResult> GetBusLocations(string sessionId, string deviceId, CancellationToken cancellationToken);
        Task<GetJourneysResult> GetJourneys(int originId, int destinationId, DateTime departureDate, string sessionId, string deviceId, CancellationToken cancellationToken);
    }
}
