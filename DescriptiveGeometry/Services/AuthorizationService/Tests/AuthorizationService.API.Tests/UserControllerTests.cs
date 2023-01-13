using System.Net;
using System.Net.Http.Json;
using System.Text;
using AuthorizationService.API.ViewModels;
using AuthorizationService.DAL.Context;
using Newtonsoft.Json;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using static AuthorizationService.API.Tests.Entities.TestUserEntity;
using static AuthorizationService.API.Tests.ViewModels.TestUserViewModel;
using static AuthorizationService.API.Tests.Constants.ApiTestsConstants;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationService.API.Tests;

public class UserControllerTests
{
    [Fact]
    public async Task Get_ReturnUserViewModel()
    {
        await using var application = new UserApi();
        var client = await CreateClient(application);

        foreach (var validUserEntity in ValidUserEntities )
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
        await using var application = new UserApi();
        var client = await CreateClient(application);

        var responseUserViewModels = await client.GetFromJsonAsync<IEnumerable<UserViewModel>>(UserPath);

        Assert.Equal(ValidUserEntities.Count(), responseUserViewModels?.Count());
    }

    [Fact]
    public async Task Create_ReturnsUserViewModel()
    {
        await using var application = new UserApi();
        var client = await CreateClient(application);

        var jsonUserViewModel = JsonConvert.SerializeObject(ValidChangeUserViewModel);
        var contentUserViewModel = new StringContent(jsonUserViewModel, Encoding.UTF8, "application/json");

        //var actualAttribute = application.Services.GetType().GetMethod("Create").GetCustomAttributes(typeof(AuthorizeAttribute), true);
        //var actualAttribute = application.Services.GetType().GetMethod("Create").GetCustomAttributes(typeof(InterceptAttribute), true);

        var response = await client.PostAsync(UserPath, contentUserViewModel);
        var responseUserViewModel = await response.Content.ReadAsAsync<UserViewModel>();

        Assert.Equal( HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(ValidChangeUserViewModel.Email, responseUserViewModel?.Email);
    }

    [Fact]
    public async Task Update_ReturnsUserViewModel()
    {
        await using var application = new UserApi();
        var client = await CreateClient(application);

        var updateUserViewModel = ValidChangeUserViewModel;
        updateUserViewModel.Email += "Updated";

        var jsonUserViewModel = JsonConvert.SerializeObject(updateUserViewModel);
        var contentUserViewModel = new StringContent(jsonUserViewModel, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{UserPath}/{ValidUserViewModel.Id}", contentUserViewModel);
        var responseUserViewModel = await response.Content.ReadAsAsync<UserViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(updateUserViewModel.Email, responseUserViewModel?.Email);
    }

    [Fact]
    public async Task Delete_ReturnsUsersCount()
    {
        await using var application = new UserApi();
        var client = await CreateClient(application);

        var responseDelete = await client.DeleteAsync($"{UserPath}/{ValidUserViewModel.Id}");
        var responseUserViewModels = await client.GetFromJsonAsync<IEnumerable<UserViewModel>>(UserPath);

        Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        Assert.Equal(ValidUserEntities.Count() - 1, responseUserViewModels?.Count());
    }

    private static async Task<HttpClient> CreateClient(UserApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Users.AddRangeAsync(ValidUserEntities);
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }
}