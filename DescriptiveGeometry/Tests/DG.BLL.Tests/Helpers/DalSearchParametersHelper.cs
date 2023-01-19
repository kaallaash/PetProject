using DG.Core.Models;
using DG.DAL.Models;

namespace DG.BLL.Tests.Helpers;

public class DalSearchParametersHelper
{
    public static SearchParameters Create(int randomValue)
    {
        return new SearchParameters()
        {
            Skip = randomValue,
            Take = randomValue
        };
    }
}