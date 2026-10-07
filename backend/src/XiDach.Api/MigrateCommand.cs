using Microsoft.EntityFrameworkCore;
using XiDach.Infrastructure.Persistence;

namespace XiDach.Api;

/// <summary>
/// `dotnet XiDach.Api.dll migrate` applies pending migrations and exits. The `migrator` container runs it once
/// before the API starts (MAIN-05).
/// </summary>
internal static class MigrateCommand
{
    public const string Name = "migrate";

    public static async Task RunAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<AppDbContext>().Database;
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(MigrateCommand));

        var pending = (await database.GetPendingMigrationsAsync(cancellationToken)).ToList();
        logger.LogInformation("Applying {Count} pending migration(s): {Migrations}", pending.Count, pending);
        await database.MigrateAsync(cancellationToken);
        logger.LogInformation("Database schema is up to date");
    }
}
