using Microsoft.EntityFrameworkCore;

namespace XiDach.Infrastructure.Persistence;

/// <summary>
/// The single EF Core context. Tables are added sprint by sprint following the system design, section 6.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options);
