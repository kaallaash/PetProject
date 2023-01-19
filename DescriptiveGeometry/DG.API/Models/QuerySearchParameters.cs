namespace DG.API.Models;

public class QuerySearchParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchPhrase { get; set; }
}