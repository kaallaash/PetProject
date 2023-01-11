using AuthorizationService.DAL.Interfaces.Repositories;
using AuthorizationService.DAL.Context;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthorizationService.DAL.Repositories;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.DAL.DI;

public static class DataAccessRegister
{
    public static void AddDataContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository<UserEntity>, UserRepository>();

        services.AddDbContext<DatabaseContext>(op =>
        {
            op.UseSqlServer(
                configuration.GetConnectionString("UserAuthorizationDb"));
        });
    }
}