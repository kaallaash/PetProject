using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Models.Enums;

namespace AuthorizationService.BLL.Tests.Helpers;

public static class UserModelHelper
{
    public static User Create(int id) => new User()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        Role = Role.DefaultUser
    };
}