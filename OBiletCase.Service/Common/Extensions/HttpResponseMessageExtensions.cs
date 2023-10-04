using Newtonsoft.Json;

namespace OBiletCase.Service.Common.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadJsonToAsync<T>(this HttpResponseMessage message, CancellationToken cancellationToken) where T : class
        {
            string contentString = await message.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<T>(contentString)!;
        }
    }
}
