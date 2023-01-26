using System.Net;
using System.Text;
using AuthorizationService.API.ViewModels;
using Newtonsoft.Json;
using Xunit;
using static AuthorizationService.API.Tests.Constants.ApiTestsConstants;
using static AuthorizationService.API.Tests.Helpers.ClientBuilder;
using static AuthorizationService.API.Tests.Entities.TestUserEntity;
using System.Net.Http.Json;
using AuthorizationService.API.Models;

namespace AuthorizationService.API.Tests;

public class TokenControllerTests
{
    [Fact]
    public async Task Login_ValidLoginViewModel_ReturnsToken()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validUserEntity in GetValidUserEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validUserEntity.Email,
                Password = validUserEntity.Password
            };
            
            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var userViewModelContent = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, userViewModelContent);

            var token = await tokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            Assert.Equal(HttpStatusCode.OK, tokenResponse.StatusCode);
            Assert.NotNull(token);
        }
    }

    [Fact]
    public async Task RefreshToken_ValidTokenModel_ReturnsToken()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validUserEntity in GetValidUserEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validUserEntity.Email,
                Password = validUserEntity.Password
            };

            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var userViewModelContent = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, userViewModelContent);
            var token = await tokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            var jsonTokenModel = JsonConvert.SerializeObject(token);
            var tokenContent = new StringContent(jsonTokenModel, Encoding.UTF8, "application/json");

            var refreshTokenResponse = await client.PostAsync(RefreshTokenPath, tokenContent);
            var refreshToken = await refreshTokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            Assert.Equal(HttpStatusCode.OK, refreshTokenResponse.StatusCode);
            Assert.NotNull(refreshToken);
        }
    }
}