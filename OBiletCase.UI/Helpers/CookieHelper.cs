using System.Text.Json;

namespace OBiletCase.UI.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie<T>(HttpContext context, string key, T value)
        {
            context.Response.Cookies.Append(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(HttpContext context, string key)
        {
            var value = context.Request.Cookies[key];
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
