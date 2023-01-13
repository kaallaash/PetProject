using AuthorizationService.API.ViewModels;
using AuthorizationService.API.Tests.Helpers;

namespace AuthorizationService.API.Tests.ViewModels;

public static class TestUserViewModel
{
    public static ChangeUserViewModel ValidChangeUserViewModel = UserModelHelper.CreateChangeUserViewModel(1);
    public static UserViewModel ValidUserViewModel = UserModelHelper.CreateUserViewModel(1);
    public static UserViewModel ValidCreateUserViewModel = UserModelHelper.CreateUserViewModel(6);

    public static IEnumerable<ChangeUserViewModel> ValidChangeUserViewModels = new List<ChangeUserViewModel>()
    {
        UserModelHelper.CreateChangeUserViewModel(1),
        UserModelHelper.CreateChangeUserViewModel(2),
        UserModelHelper.CreateChangeUserViewModel(3),
        UserModelHelper.CreateChangeUserViewModel(4),
        UserModelHelper.CreateChangeUserViewModel(5)
    };

    public static IEnumerable<UserViewModel> ValidUserViewModels = new List<UserViewModel>()
    {
        UserModelHelper.CreateUserViewModel(1),
        UserModelHelper.CreateUserViewModel(2),
        UserModelHelper.CreateUserViewModel(3),
        UserModelHelper.CreateUserViewModel(4),
        UserModelHelper.CreateUserViewModel(5)
    };
}