using DG.DAL.Models;
using DG.BLL.Tests.Helpers;

namespace DG.BLL.Tests.Models
{
    internal class TestDalSearchParametersModel
    {
        public static SearchParameters GetValidDalSearchParametersModel => DalSearchParametersHelper.Create(1);

        public static IEnumerable<SearchParameters> GetValidDalSearchParametersModels => new List<SearchParameters>()
        {
            DalSearchParametersHelper.Create(1),
            DalSearchParametersHelper.Create(2),
            DalSearchParametersHelper.Create(3),
            DalSearchParametersHelper.Create(4),
            DalSearchParametersHelper.Create(5),
        };
    }
}
