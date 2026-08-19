using OmniSedeBackend.Attributes;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Security;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
    {
        var endpoint = context.GetEndpoint();
        
        var requireRole = endpoint?.Metadata.GetMetadata<RequireRoleAttribute>();
        var requireAuth = endpoint?.Metadata.GetMetadata<RequireAuthAttribute>() is not null || requireRole is not null;

        if (!requireAuth)
        {
            await _next(context);
            return;
        }

        var token = ExtractTokenFromHeader(context);

        if (string.IsNullOrEmpty(token))
        {
            await WriteProblem(context, StatusCodes.Status401Unauthorized, "Token mancante. Aggiungi l'header 'Authorization: Bearer <token>'.");
            return;
        }

        var principal = jwtService.ValidateToken(token);

        if (principal is null)
        {
            await WriteProblem(context, StatusCodes.Status401Unauthorized, "Token non valido o scaduto.");
            return;
        }
        
        context.User = principal;
        
        if (requireRole is not null && !context.User.IsInRole(requireRole.Role))
        {
            await WriteProblem(context, StatusCodes.Status403Forbidden,"Accesso negato: è richiesto il ruolo " + requireRole.Role);
            return;
        }

        await _next(context);
    }

    private static string? ExtractTokenFromHeader(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) ||
            !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return authHeader["Bearer ".Length..].Trim();
    }

    private static async Task WriteProblem(HttpContext context, int statusCode, string detail)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new
        {
            status = statusCode,
            title = statusCode == StatusCodes.Status401Unauthorized ? "Unauthorized" : "Forbidden",
            detail
        });
    }
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}