using System.Net;
using System.Text;
using AuthorizationService.API.ViewModels;
using Newtonsoft.Json;
using Xunit;
using static AuthorizationService.API.Tests.Entities.TestUserEntity;
using static AuthorizationService.API.Tests.ViewModels.TestUserViewModel;
using static AuthorizationService.API.Tests.Constants.ApiTestsConstants;
using static AuthorizationService.API.Tests.Helpers.ClientBuilder;
using System.Net.Http.Json;

namespace AuthorizationService.API.Tests;

public class UserControllerTests
{
    [Fact]
    public async Task Get_ReturnUserViewModel()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validUserEntity in GetValidUserEntities )
        {
            var responseUserViewModel =
                await client.GetFromJsonAsync<UserViewModel>($"{UserPath}/{validUserEntity.Id}");

            var check = responseUserViewModel;

            Assert.Equal(validUserEntity.Id, responseUserViewModel?.Id);
        }
    }

    [Fact]
    public async Task GetAll_ReturnsUserViewModels()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        var responseUserViewModels = await client.GetFromJsonAsync<IEnumerable<UserViewModel>>(UserPath);

        Assert.Equal(GetValidUserEntities.Count(), responseUserViewModels?.Count());
    }

    [Fact]
    public async Task Create_ReturnsUserViewModel()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        var validCreatedUserViewModel = GetValidCreatedUserViewModel;
        var jsonUserViewModel = JsonConvert.SerializeObject(validCreatedUserViewModel);
        var contentUserViewModel = new StringContent(jsonUserViewModel, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(UserPath, contentUserViewModel);
        var responseUserViewModel = await response.Content.ReadAsAsync<UserViewModel>();

        Assert.Equal( HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(validCreatedUserViewModel.Email, responseUserViewModel?.Email);
    }

    [Fact]
    public async Task Update_ReturnsUserViewModel()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        var updateUserViewModel = GetValidUpdatedUserViewModel;
        updateUserViewModel.Email += "Updated";

        var jsonUserViewModel = JsonConvert.SerializeObject(updateUserViewModel);
        var contentUserViewModel = new StringContent(jsonUserViewModel, Encoding.UTF8, "application/json");
        
        var response = await client.PutAsync($"{UserPath}/{GetValidUserViewModel.Id}", contentUserViewModel);
        var responseUserViewModel = await response.Content.ReadAsAsync<UserViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(updateUserViewModel.Email, responseUserViewModel?.Email);
    }

    [Fact]
    public async Task Delete_ReturnsUsersCount()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        var responseDelete = await client.DeleteAsync($"{UserPath}/{GetValidUserViewModel.Id}");
        var responseUserViewModels = await client.GetFromJsonAsync<IEnumerable<UserViewModel>>(UserPath);

        Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        Assert.Equal(GetValidUserEntities.Count() - 1, responseUserViewModels?.Count());
    }
}