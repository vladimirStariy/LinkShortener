using Microsoft.AspNetCore.WebUtilities;
using shortid;

namespace LinkShortener.Middleware
{
    public static class UrlShortener
    {
        public static string ShortenUrl(int id, string long_url)
        {
            Uri.TryCreate(long_url, UriKind.Absolute, out Uri result);
            var url = result.ToString();
            return GetShortenUrlById(id);
        }

        private static string GetShortenUrlById(int id)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
        }

        private static int GetUrlId(string shortenChunk)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(shortenChunk));
        }
    }
}
