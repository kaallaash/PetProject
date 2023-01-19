using DG.BLL.Models;
using DG.Core.Models;

namespace DG.BLL.Tests.Helpers;

public static class BllSearchParametersHelper
{
    public static SearchParameters Create(int randomValue)
    {
        return new SearchParameters()
        {
            PageInfo = new PageInfo(randomValue, randomValue * randomValue)
        };
    }
}