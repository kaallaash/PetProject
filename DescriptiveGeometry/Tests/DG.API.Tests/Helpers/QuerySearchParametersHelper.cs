using DG.API.Models;

namespace DG.API.Tests.Helpers;

public static class QuerySearchParametersHelper
{
    public static QuerySearchParameters Create(int id) => new QuerySearchParameters()
    {
        PageNumber = id,
        PageSize = id,
        SearchPhrase = id.ToString()
    };
}