namespace MetrosoftSearch.Mvc.Dtos;

public class SearchResultDto
{
    public int Id { get; set; }
    public string SearchPhrase { get; set; }
    public string Url { get; set; }
    public string Positions { get; set; }
    public bool IsFound { get; set; }
    public DateTime SearchDate { get; set; }
    public string Notes { get; set; }
}
