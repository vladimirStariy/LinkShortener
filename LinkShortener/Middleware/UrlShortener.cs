using Base62;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using shortid;
using ShortUrl;

namespace LinkShortener.Middleware
{
    public static class UrlShortener
    {
        public static string ShortenUrl(int id, string long_url)
        {
            Uri uriResult;
            bool isValid = Uri.TryCreate(long_url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isValid)
            {
                var converter = new Base62Converter();
                byte[] encodedStr = converter.Encode(BitConverter.GetBytes(id));
                var shorten = BitConverter.ToString(encodedStr);
                shorten = shorten.Replace("-", "");
                var UrlShorten = new ShortUrl.UrlShortener(CharacterSet.Base62NumbersUpperLower);
                var result = UrlShorten.Convert(shorten).BaseValue.ToString();
                return result.ToLower();
            }
            else
            {
                throw new ArgumentException("Некорректная ссылка");
            }
        }

        public static bool ValidateUrl(string url)
        {
            Uri uriResult;
            bool isValid = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValid;
        }
    }
}
