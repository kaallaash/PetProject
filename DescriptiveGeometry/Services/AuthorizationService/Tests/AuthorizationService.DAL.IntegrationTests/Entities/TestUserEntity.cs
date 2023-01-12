using AuthorizationService.DAL.IntegrationTests.Helpers;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.DAL.IntegrationTests.Entities;

public static class TestUserEntity
{
    internal static IEnumerable<UserEntity> GetValidUserEntitiesWithId() => new List<UserEntity>()
    {
        UserEntityHelper.CreateValidEntity(1),
        UserEntityHelper.CreateValidEntity(2),
        UserEntityHelper.CreateValidEntity(3),
        UserEntityHelper.CreateValidEntity(4),
        UserEntityHelper.CreateValidEntity(5)
    };

    internal static IEnumerable<UserEntity> GetValidCreatedUserEntities() => new List<UserEntity>()
        {
        UserEntityHelper.CreateValidEntityWithoutId(),
        UserEntityHelper.CreateValidEntityWithoutId(),
        UserEntityHelper.CreateValidEntityWithoutId(),
        UserEntityHelper.CreateValidEntityWithoutId(),
        UserEntityHelper.CreateValidEntityWithoutId()
        };

    public static IEnumerable<object[]> GetValidUserEntities()
    {
        foreach (var validCreatedUserEntity in GetValidCreatedUserEntities())
        {
            yield return new object[] { validCreatedUserEntity };
        }
    }
}
