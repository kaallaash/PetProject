using AuthorizationService.API.ViewModels;
using AuthorizationService.API.Tests.Helpers;

namespace AuthorizationService.API.Tests.ViewModels;

public static class TestUserViewModel
{
    public static ChangeUserViewModel GetValidUpdatedUserViewModel => UserViewModelHelper.CreateChangeUserViewModel(1);
    public static ChangeUserViewModel GetValidCreatedUserViewModel => UserViewModelHelper.CreateChangeUserViewModel(6);
    public static UserViewModel GetValidUserViewModel => UserViewModelHelper.CreateUserViewModel(1);
}