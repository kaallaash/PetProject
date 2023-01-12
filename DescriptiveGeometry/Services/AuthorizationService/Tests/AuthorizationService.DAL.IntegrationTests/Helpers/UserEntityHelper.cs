using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Entities.Enums;

namespace AuthorizationService.DAL.IntegrationTests.Helpers;

public static class UserEntityHelper
{
    public static UserEntity CreateValidEntity(int id) => new UserEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        Role = Role.DefaultUser
    };

    public static UserEntity CreateValidEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new UserEntity()
        {
            Name = $"Name{number}",
            Email = $"Email{number}@mail.com",
            Password = $"password{number}",
            Role = Role.DefaultUser
        };
    }
}