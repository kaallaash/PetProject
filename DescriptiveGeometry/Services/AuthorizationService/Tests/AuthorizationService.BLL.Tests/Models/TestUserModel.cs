using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Tests.Helpers;

namespace AuthorizationService.BLL.Tests.Models;

public static class TestUserModel
{
    public static User ValidUserModel = UserModelHelper.Create(1);

    public static IEnumerable<User> ValidUserModels = new List<User>()
    {
        UserModelHelper.Create(1),
        UserModelHelper.Create(2),
        UserModelHelper.Create(3),
        UserModelHelper.Create(4),
        UserModelHelper.Create(5)
    };
}