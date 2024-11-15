using MetrosoftSearch.Api.Data.Dtos;
using MetrosoftSearch.Api.Data.Response;

namespace MetrosoftSearch.Api.Services.Interfaces;

public interface ISearchService
{
    Task<GenericResponse<List<SearchResultDto>>> PerformSearchAsync(string searchPhrase, string url);
    Task<GenericResponse<List<SearchResultDto>>> GetSearchHistoryAsync(string? searchPhrase = null, string? url = null);
}
