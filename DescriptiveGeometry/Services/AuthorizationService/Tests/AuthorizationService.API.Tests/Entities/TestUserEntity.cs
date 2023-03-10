using AuthorizationService.API.Tests.Helpers;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.API.Tests.Entities;

public static class TestUserEntity
{
    public static IEnumerable<UserEntity> GetValidUserEntities => new List<UserEntity>()
    {
        UserEntityHelper.Create(1),
        UserEntityHelper.Create(2),
        UserEntityHelper.Create(3),
        UserEntityHelper.Create(4),
        UserEntityHelper.Create(5)
    };
}
