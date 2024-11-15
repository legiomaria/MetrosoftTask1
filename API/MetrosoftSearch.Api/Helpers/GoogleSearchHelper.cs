using System.Text.RegularExpressions;

namespace MetrosoftSearch.Api.Helpers;

public partial class GoogleSearchHelper
{
    public static List<string> ParseUrlsFromHtml(string htmlContent)
    {
        var urls = new List<string>();


        var regex = MyRegex();

        
        var matches = regex.Matches(htmlContent);

        foreach (Match match in matches)
        {
            
            if (match.Groups["url"].Success)
            {
                var url = match.Groups["url"].Value;
                urls.Add(url);
            }
        }

        return urls;
    }

    [GeneratedRegex(@"<a[^>]*href=[""'](/url\?q=)(?<url>https?://[^&""']+)[""']", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MyRegex();
}
