using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Entities.Enums;

namespace AuthorizationService.BLL.Tests.Helpers;

public static class UserEntityHelper
{
    public static UserEntity Create(int id) => new UserEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        Role = Role.DefaultUser,
        DateCreated = DateTimeOffset.Now,
        DateUpdated = DateTimeOffset.Now
    };
}