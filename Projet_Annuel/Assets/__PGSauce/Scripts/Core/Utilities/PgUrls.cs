using System.Text;
using PGSauce.Localization;

namespace PGSauce.Core.Utilities
{
    public static class PgUrls
    {
        public static string TryGetLocalizedUrl(string url)
        {
            if (!url.Contains("bigcatto.com"))
            {
                return url;
            }

            var sb = new StringBuilder();
            sb.Append(url);

            if (!url.Contains("?"))
            {
                sb.Append("?");
            }

            sb.Append($"lang={PgLocalization.CurrentLanguageCode}");

            return sb.ToString();
        }
    }
}