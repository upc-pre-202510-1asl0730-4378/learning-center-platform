using eb4341u202318323.API.Hr.Infrastructure.Persistence.EFC.Configuration.Extensions;
using eb4341u202318323.API.Maintenance.Infrastructure.Persistence.EFC.Configuration.Extensions;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyMaintenanceConfiguration();
        builder.ApplyHrConfiguration();
        // Apply Naming convention Policy
        builder.UseSnakeCaseNamingConvention();
        
        
    }
}