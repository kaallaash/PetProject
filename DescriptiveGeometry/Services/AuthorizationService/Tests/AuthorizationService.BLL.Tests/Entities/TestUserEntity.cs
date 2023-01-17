using AuthorizationService.BLL.Tests.Helpers;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.BLL.Tests.Entities;

public static class TestUserEntity
{
    public static UserEntity ValidUserEntity = UserEntityHelper.Create(1);

    public static IEnumerable<UserEntity> ValidUserEntities = new List<UserEntity>()
    {
        UserEntityHelper.Create(1),
        UserEntityHelper.Create(2),
        UserEntityHelper.Create(3),
        UserEntityHelper.Create(4),
        UserEntityHelper.Create(5)
    };
}
