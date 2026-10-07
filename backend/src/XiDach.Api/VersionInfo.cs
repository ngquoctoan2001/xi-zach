using System.Reflection;

namespace XiDach.Api;

internal sealed record VersionInfo(string Name, string Version, string Environment);

internal static class AppVersion
{
    public static string Current { get; } =
        typeof(AppVersion).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? "0.0.0";
}
