using AuthorizationService.API.ViewModels;
using AuthorizationService.API.Tests.Helpers;

namespace AuthorizationService.API.Tests.ViewModels;

public static class TestUserViewModel
{
    public static ChangeUserViewModel ValidUpdatedUserViewModel = UserViewModelHelper.CreateChangeUserViewModel(1);
    public static ChangeUserViewModel ValidCreatedUserViewModel = UserViewModelHelper.CreateChangeUserViewModel(6);
    public static UserViewModel ValidUserViewModel = UserViewModelHelper.CreateUserViewModel(1);
}