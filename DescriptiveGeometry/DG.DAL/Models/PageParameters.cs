namespace DG.DAL.Models;

public class PageParameters
{
    private const int MaxPageSize = 50;
    private const int MinPageSize = 1;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PageParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        PageSize = pageSize < MinPageSize ? MinPageSize : pageSize;
    }
}
