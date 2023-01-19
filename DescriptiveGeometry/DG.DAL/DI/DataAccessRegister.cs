using DG.DAL.Context;
using DG.DAL.Entities;
using DG.DAL.Interfaces.Repositories;
using DG.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DG.DAL.DI;

public static class DataAccessRegister
{
    public static void AddDataContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDrawingRepository<DrawingEntity>, DrawingRepository>();

        services.AddDbContext<DatabaseContext>(op =>
        {
            op.UseSqlServer(
                configuration.GetConnectionString("DescriptiveGeometryDb"));
        });
    }
}