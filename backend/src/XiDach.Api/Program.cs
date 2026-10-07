using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using XiDach.Api;
using XiDach.Api.Observability;
using XiDach.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var seqServerUrl = builder.Configuration["Seq:ServerUrl"];
    builder.Services.AddSerilog((services, logger) =>
    {
        logger
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "XiDach.Api")
            .WriteTo.Console(new RenderedCompactJsonFormatter());
        if (!string.IsNullOrWhiteSpace(seqServerUrl))
        {
            logger.WriteTo.Seq(seqServerUrl);
        }
    });

    var connectionString = builder.Configuration.GetConnectionString("Default");
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("Missing connection string 'ConnectionStrings:Default'.");
    }

    builder.Services.AddInfrastructure(connectionString);
    builder.Services.AddProblemDetails();
    builder.Services.AddOpenApi();

    var app = builder.Build();

    if (args.Contains(MigrateCommand.Name))
    {
        await MigrateCommand.RunAsync(app.Services);
        return 0;
    }

    app.UseExceptionHandler();
    app.UseStatusCodePages();
    app.UseCorrelationId();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    // OPS-02: /health/live only says the process is up; /health/ready also checks the database.
    app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains(DependencyInjection.ReadinessTag),
    });

    app.MapGet("/api/version", (IHostEnvironment environment) =>
        new VersionInfo("xi-dach", AppVersion.Current, environment.EnvironmentName));

    await app.RunAsync();
    return 0;
}
catch (Exception exception) when (exception is not HostAbortedException)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

/// <summary>Entry point, exposed so integration tests can host the API in memory.</summary>
public partial class Program;
