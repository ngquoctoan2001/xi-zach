using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XiDach.Infrastructure.Persistence;

namespace XiDach.Infrastructure;

public static class DependencyInjection
{
    /// <summary>Health checks tagged with this value are part of /health/ready.</summary>
    public const string ReadinessTag = "ready";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<AppDbContext>(options => ConfigureDbContext(options, connectionString));
        services.AddHealthChecks().AddDbContextCheck<AppDbContext>("database", tags: [ReadinessTag]);
        return services;
    }

    internal static void ConfigureDbContext(DbContextOptionsBuilder options, string connectionString) =>
        options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
}
