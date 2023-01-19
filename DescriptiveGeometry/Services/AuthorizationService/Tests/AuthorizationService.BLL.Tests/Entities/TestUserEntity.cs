using AuthorizationService.BLL.Tests.Helpers;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.BLL.Tests.Entities;

public static class TestUserEntity
{
    public static UserEntity GetValidUserEntity => UserEntityHelper.Create(1);

    public static IEnumerable<UserEntity> GetValidUserEntities => new List<UserEntity>()
    {
        UserEntityHelper.Create(1),
        UserEntityHelper.Create(2),
        UserEntityHelper.Create(3),
        UserEntityHelper.Create(4),
        UserEntityHelper.Create(5)
    };
}
