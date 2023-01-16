using System.Net;
using System.Text;
using AuthorizationService.API.ViewModels;
using Newtonsoft.Json;
using Xunit;
using static AuthorizationService.API.Tests.Constants.ApiTestsConstants;
using static AuthorizationService.API.Tests.Helpers.ClientBuilder;
using static AuthorizationService.API.Tests.Entities.TestUserEntity;

namespace AuthorizationService.API.Tests;

public class TokenControllerTests
{
    [Fact]
    public async Task Post_ValidLoginViewModel_ReturnsToken()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validUserEntity in ValidUserEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validUserEntity.Email,
                Password = validUserEntity.Password
            };

            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var contentUserViewModel = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(TokenPath, contentUserViewModel);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}