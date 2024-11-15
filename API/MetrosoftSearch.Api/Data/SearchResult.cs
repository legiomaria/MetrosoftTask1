using System.ComponentModel.DataAnnotations;

namespace MetrosoftSearch.Api.Data;
public class SearchResult
{
    public int Id { get; set; }
    public string SearchPhrase { get; set; }
    public string Url { get; set; }
    [StringLength(200)]
    public string Positions { get; set; }
    public bool IsFound { get; set; }
    public DateTime SearchDate { get; set; }
    public string Notes { get; set; }
}
