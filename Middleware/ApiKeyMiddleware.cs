// Middleware/ApiKeyMiddleware.cs
namespace ClientesApi.Middleware;
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEYNAME = "X-API-KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key no fue proporcionada.");
            return;
        }

        var appApiKey = config.GetValue<string>("ApiKey");

        // Use string.Equals for a null-safe, case-sensitive comparison.
        if (!string.Equals(appApiKey, extractedApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key no es válida.");
            return;
        }

        await _next(context);
    }
}