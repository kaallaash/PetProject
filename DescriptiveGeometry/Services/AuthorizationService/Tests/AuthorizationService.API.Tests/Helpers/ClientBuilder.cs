using AuthorizationService.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static AuthorizationService.API.Tests.Entities.TestUserEntity;

namespace AuthorizationService.API.Tests.Helpers;

internal static class ClientBuilder
{
    internal static async Task<HttpClient> CreateClient(AuthorizationApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Users.AddRangeAsync(GetValidUserEntities);
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }

}