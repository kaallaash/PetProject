using AuthorizationService.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.DAL.Context;

public class DatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (base.Database.IsRelational())
        {
            base.Database.Migrate();
        }
    }
}