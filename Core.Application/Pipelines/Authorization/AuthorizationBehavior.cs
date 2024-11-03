using MediatR;
using Microsoft.AspNetCore.Http;
using Core.Security.Extensions;
using Core.Security.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
          _httpContextAccessor = httpContextAccessor;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userRoleClaims = _httpContextAccessor.HttpContext.User.GetRoleClaims();
        if (userRoleClaims == null)
            throw new AuthorizationException("You are not authenticated.");

        var roleCheck = userRoleClaims
             .FirstOrDefault(
                 userRoleClaim =>  request.Roles.Any(role => role == userRoleClaim) //userRoleClaim == GeneralOperationClaims.Admin ||
             );
     bool isNotMatchedAUserRoleClaimWithRequestRoles = roleCheck != null;
        if (!isNotMatchedAUserRoleClaimWithRequestRoles)
            throw new AuthorizationException("You are not authorized.");

        TResponse response = await next();
        return response;
    }
}
