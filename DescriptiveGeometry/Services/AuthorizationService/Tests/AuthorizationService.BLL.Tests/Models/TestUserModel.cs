using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Tests.Helpers;

namespace AuthorizationService.BLL.Tests.Models;

public static class TestUserModel
{
    public static User GetValidUserModel => UserModelHelper.Create(1);

    public static IEnumerable<User> GetValidUserModels => new List<User>()
    {
        UserModelHelper.Create(1),
        UserModelHelper.Create(2),
        UserModelHelper.Create(3),
        UserModelHelper.Create(4),
        UserModelHelper.Create(5)
    };
}