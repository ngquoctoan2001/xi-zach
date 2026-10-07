using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace XiDach.Api.Tests;

/// <summary>
/// Hosts the API in memory. These checks never touch the database, so no PostgreSQL is needed here;
/// database-backed tests use Testcontainers from sprint S2 on.
/// </summary>
public sealed class ApiSmokeTests(ApiSmokeTests.Factory factory) : IClassFixture<ApiSmokeTests.Factory>
{
    public sealed class Factory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.UseSetting("ConnectionStrings:Default", "Host=localhost;Database=unused;Username=unused;Password=unused");
        }
    }

    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task OPS02_LiveHealthCheck_ReportsHealthyWithoutDatabase()
    {
        var response = await _client.GetAsync(new Uri("/health/live", UriKind.Relative));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        (await response.Content.ReadAsStringAsync()).ShouldBe("Healthy");
    }

    [Fact]
    public async Task OPS01_EveryResponse_CarriesCorrelationId()
    {
        var response = await _client.GetAsync(new Uri("/health/live", UriKind.Relative));

        response.Headers.TryGetValues("X-Correlation-Id", out var values).ShouldBeTrue();
        values!.Single().ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Version_ReturnsApplicationNameAndEnvironment()
    {
        var version = await _client.GetFromJsonAsync<VersionDto>(new Uri("/api/version", UriKind.Relative));

        version.ShouldNotBeNull();
        version.Name.ShouldBe("xi-dach");
        version.Environment.ShouldBe("Testing");
    }

    [Fact]
    public async Task UnknownRoute_ReturnsProblemDetails()
    {
        var response = await _client.GetAsync(new Uri("/api/does-not-exist", UriKind.Relative));

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        response.Content.Headers.ContentType?.MediaType.ShouldBe("application/problem+json");
    }

    private sealed record VersionDto(string Name, string Version, string Environment);
}
