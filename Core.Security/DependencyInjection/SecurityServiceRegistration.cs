using Core.Security.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Security.DependencyInjection;


public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices<TUserId, TOperationClaimId, TRefreshTokenId>(this IServiceCollection services, TokenOptions tokenOptions)
    {
        TokenOptions tokenOptions2 = tokenOptions;
        services.AddScoped<ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>, JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId>>((IServiceProvider _) => new JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId>(tokenOptions2));
        return services;
    }
}