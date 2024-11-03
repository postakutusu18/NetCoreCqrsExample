using System.Security.Claims;

namespace WebApi.Middlewares;
public class AddClaimsMiddleware
{
    private readonly RequestDelegate _next;

    public AddClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Write")
        };

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var userPrincipal = new ClaimsPrincipal(identity);

        context.User = userPrincipal;

        await _next(context);
    }
}