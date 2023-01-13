using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Entities.Enums;

namespace AuthorizationService.API.Tests.Helpers;

public static class UserEntityHelper
{
    public static UserEntity Create(int id) => new UserEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Name{id}",
        Password = $"Name{id}",
        Role = Role.DefaultUser
    };
}