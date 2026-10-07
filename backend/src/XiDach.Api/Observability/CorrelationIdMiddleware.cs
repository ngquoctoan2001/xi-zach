using System.Diagnostics;
using Serilog.Context;

namespace XiDach.Api.Observability;

/// <summary>
/// OPS-01: every log line written while handling a request carries the same CorrelationId, and the id is
/// returned to the browser so a user-reported problem can be found in Seq.
/// </summary>
internal static class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-Id";

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app) =>
        app.Use(async (context, next) =>
        {
            var correlationId = Activity.Current?.TraceId.ToHexString() ?? context.TraceIdentifier;
            context.Response.Headers[HeaderName] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next(context);
            }
        });
}
