namespace DG.Core.Models;

public class PageInfo
{
    private const int MaxPageSize = 50;
    private const int MinPageSize = 1;
    private const int MinPageNumber = 1;
    private int _pageNumber = 1;
    private int _pageSize = 5;

    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            if (value >= MinPageNumber)
            {
                _pageNumber = value;
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value is >= MinPageSize and <= MaxPageSize)
            {
                _pageSize = value;
            }
        }
    }

    public PageInfo(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}