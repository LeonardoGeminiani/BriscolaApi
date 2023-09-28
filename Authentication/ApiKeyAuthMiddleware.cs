namespace ApiKey2.Authentication;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate next;
    private readonly IConfiguration configuration;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        this.next = next;
        this.configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var keyValue))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key Missing");
            return;
        }

        var apiKey = configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        if (!apiKey.Equals(keyValue))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid Api Key");
            return;
        }

        await next(context);
    }
}