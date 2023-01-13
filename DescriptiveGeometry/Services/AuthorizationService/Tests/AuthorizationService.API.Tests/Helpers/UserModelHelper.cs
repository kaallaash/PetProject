using AuthorizationService.API.ViewModels;

namespace AuthorizationService.API.Tests.Helpers;

public static class UserModelHelper
{
    public static ChangeUserViewModel CreateChangeUserViewModel(int id) => new ChangeUserViewModel()
    {
        Name = $"Name{id}",
        Email = $"Email{id}",
        Password = $"password{id}"
    };

    public static UserViewModel CreateUserViewModel(int id) => new UserViewModel()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}"
    };
}