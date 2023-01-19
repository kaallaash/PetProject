using DG.BLL.Models;
using DG.BLL.Tests.Helpers;

namespace DG.BLL.Tests.Models;

public static class TestBllSearchParametersModel
{
    public static SearchParameters GetValidBllSearchParametersModel => BllSearchParametersHelper.Create(1);

    public static IEnumerable<SearchParameters> GetValidBllSearchParametersModels => new List<SearchParameters>()
    {
        BllSearchParametersHelper.Create(1),
        BllSearchParametersHelper.Create(2),
        BllSearchParametersHelper.Create(3),
        BllSearchParametersHelper.Create(4),
        BllSearchParametersHelper.Create(5),
    };
}