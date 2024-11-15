using MetrosoftSearch.Api.Data.Response;
using MetrosoftSearch.Mvc.Constants;
using MetrosoftSearch.Mvc.Dtos;
using MetrosoftSearch.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetrosoftSearch.Mvc.Controllers;

public class SearchController(IHttpClientFactory httpClientFactory) : Controller
{
    readonly HttpClient client = httpClientFactory.CreateClient(NameConstant.ApiName);
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(SearchRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var response = await client.PostAsJsonAsync("search", request);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<GenericResponse<List<SearchResultDto>>>();
            var searchResults = apiResponse.Data;
            return View("SearchResults", searchResults);
        }

        ModelState.AddModelError(string.Empty, "Error fetching search results.");
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> History(string searchPhrase, string url)
    {
        var response = await client.GetAsync($"search/history?searchPhrase={searchPhrase}&url={url}");

        if (response.IsSuccessStatusCode)
        {
            var historyResults = await response.Content.ReadFromJsonAsync<List<SearchResultDto>>();
            return View(historyResults);
        }

        ModelState.AddModelError(string.Empty, "Error fetching search history.");
        return View(new List<SearchResultDto>());
    }
}
