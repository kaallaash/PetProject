using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Services;
using AuthorizationService.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationService.BLL.DI;

public static class BusinessLogicRegister
{
    public static void AddUsers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserService<User, int>, UserService>();
        services.AddDataContext(configuration);
    }
}