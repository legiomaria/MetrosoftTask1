using MetrosoftSearch.Api.Data.Dtos;
using MetrosoftSearch.Api.Data.Response;
using MetrosoftSearch.Api.EF;
using MetrosoftSearch.Api.Helpers;
using MetrosoftSearch.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MetrosoftSearch.Api.Data;
namespace MetrosoftSearch.Api.Services;

public class SearchService(ApplicationDbContext dbContext, IConfiguration configuration) : ISearchService
{
    readonly ApplicationDbContext _dbContext = dbContext;
    readonly IConfiguration _configuration = configuration;

    public async Task<GenericResponse<List<SearchResultDto>>> GetSearchHistoryAsync(string? searchPhrase = null, string? url = null)
    {
        // Start query on the SearchResults table
        var query = _dbContext.SearchResults.AsQueryable();

        // Apply filters if provided
        if (!string.IsNullOrEmpty(searchPhrase))
        {
            query = query.Where(sr => sr.SearchPhrase.Contains(searchPhrase));
        }

        if (!string.IsNullOrEmpty(url))
        {
            query = query.Where(sr => sr.Url.Contains(url));
        }

        // Fetch results ordered by SearchDate for trend analysis
        var searchHistory = await query
            .OrderByDescending(sr => sr.SearchDate)
            .Select(sr => new SearchResultDto
            {
                Id = sr.Id,
                SearchPhrase = sr.SearchPhrase,
                Url = sr.Url,
                Positions = sr.Positions,
                IsFound = sr.IsFound,
                SearchDate = sr.SearchDate,
                Notes = sr.Notes
            })
            .ToListAsync();

        return new GenericResponse<List<SearchResultDto>>
        {
            Data = searchHistory,
            Message = searchHistory.Any() ? "Data returned successfully" : "No data found",
            IsSuccess = true
        };
    }
    public async Task<GenericResponse<List<SearchResultDto>>> PerformSearchAsync(string searchPhrase, string url)
    {
        try
        {
            var searchResults = await ScrapeGoogleForResultsAsync(searchPhrase);
            var positions = FindUrlPositions(searchResults, url);
            var resultString = string.Join(", ", positions);
            bool isFound = !string.IsNullOrEmpty(resultString);
            resultString = string.IsNullOrEmpty(resultString) ? "0" : resultString;
            string notes = isFound ? "Link is found" : "Link not found";
            var now = DateTime.Now;
            var searchResult = new SearchResult
            {
                SearchPhrase = searchPhrase,
                Url = url,
                Positions = resultString,
                SearchDate = now,
                IsFound = isFound,
                Notes = notes
            };
            _dbContext.SearchResults.Add(searchResult);

            await _dbContext.SaveChangesAsync();
            var data = new List<SearchResultDto>
            {
                new()
                {
                    IsFound = searchResult.IsFound,
                    Notes = notes,
                    SearchDate= now,
                    SearchPhrase = searchPhrase,
                    Positions = searchResult.Positions,
                    Url = url
                }
            };
            return new GenericResponse<List<SearchResultDto>>
            {
                Message = notes,
                IsSuccess = isFound,
                Data = data
            };
        }
        catch (Exception ex)
        {

            return new GenericResponse<List<SearchResultDto>>
            {
                Message = ex.Message,
                IsSuccess = false,
                Data = []
            };
        }
    }
    private async Task<List<string>> ScrapeGoogleForResultsAsync(string searchPhrase)
    {
        using HttpClient client = new();
        var googleConnection = _configuration.GetConnectionString("GoogleConnection");
        var response = await client.GetStringAsync($"{googleConnection}{searchPhrase}");
        // Parse the response to find URLs
        var urls = GoogleSearchHelper.ParseUrlsFromHtml(response);
        return urls;
    }

    private List<int> FindUrlPositions(List<string> searchResults, string url)
    {
        List<int> positions = [];
        for (int i = 0; i < searchResults.Count; i++)
        {
            if (searchResults[i].Contains(url))
            {
                positions.Add(i + 1);
            }
        }
        return positions;
    }
}
