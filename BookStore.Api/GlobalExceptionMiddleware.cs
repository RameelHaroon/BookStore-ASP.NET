using System;
using System.Diagnostics;

namespace BookStore.Api;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<GlobalExceptionMiddleware> logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not process a request. TraceId: {TraceId}",
                Activity.Current?.Id
            );

            await Results.Problem(
                title: "We are working on the Problem",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    {"traceId", Activity.Current?.Id}
                }
            ).ExecuteAsync(httpContext);
        }
    }
}
