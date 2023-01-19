namespace DG.DAL.Models;

public class SearchParameters
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SearchPhrase { get; set; }
}