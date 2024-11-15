using MetrosoftSearch.Api.Data.Request;
using MetrosoftSearch.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetrosoftSearch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController(ISearchService searchService) : ControllerBase
{
    readonly ISearchService _searchService = searchService;

    [HttpPost]
    public async Task<IActionResult> Search(SearchRequest request)
    {
        var result = await _searchService.PerformSearchAsync(request.SearchPhrase, request.Url);
        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory([FromQuery] string searchPhrase)
    {
        var history = await _searchService.GetSearchHistoryAsync(searchPhrase);
        return Ok(history);
    }
}
