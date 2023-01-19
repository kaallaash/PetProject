using System.ComponentModel.DataAnnotations;
using DG.Core.Models;

namespace DG.BLL.Models;

public class SearchParameters
{
    private PageInfo _pageInfo = new PageInfo(1, 5);

    public PageInfo PageInfo
    {
        get => _pageInfo;
        set
        {
            if (value is not null)
            {
                _pageInfo = value;
            }

        }
    }
    public string? SearchPhrase { get; set; }
}