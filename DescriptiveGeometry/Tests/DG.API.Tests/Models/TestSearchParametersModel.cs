using DG.API.Models;
using DG.API.Tests.Helpers;
using DG.DAL.Entities;

namespace DG.API.Tests.Models;

public static class TestSearchParametersModel
{
    public static QuerySearchParameters GetValidSearchParametersModel => QuerySearchParametersHelper.Create(1);

    public static IEnumerable<QuerySearchParameters> GetValidSearchParametersModels => new List<QuerySearchParameters>()
    {
        QuerySearchParametersHelper.Create(1),
        QuerySearchParametersHelper.Create(2),
        QuerySearchParametersHelper.Create(3),
        QuerySearchParametersHelper.Create(4),
        QuerySearchParametersHelper.Create(5)
    };
}