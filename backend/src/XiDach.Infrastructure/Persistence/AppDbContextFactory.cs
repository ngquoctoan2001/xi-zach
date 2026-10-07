using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace XiDach.Infrastructure.Persistence;

/// <summary>
/// Lets `dotnet ef` build the model without starting the web host. The connection string is only a
/// placeholder: creating migrations never connects to a database.
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        DependencyInjection.ConfigureDbContext(options, "Host=localhost;Database=xidach_design;Username=design;Password=design");
        return new AppDbContext(options.Options);
    }
}
