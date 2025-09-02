using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger _logger;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ILogger logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var userRoleClaims = _httpContextAccessor.HttpContext.User.GetRoleClaims();
        if (userRoleClaims == null)
        {
            string message = "You are not authenticated.";
            LogDetail value = _logDetail(request, next,message);
            _logger.Critical(JsonConvert.SerializeObject(value));
            throw new AuthorizationException(message);
        }

        var roleCheck = userRoleClaims
             .FirstOrDefault(
                 userRoleClaim => request.Roles.Any(role => userRoleClaim == GeneralOperationClaims.Admin || role == userRoleClaim)
             );
        bool isNotMatchedAUserRoleClaimWithRequestRoles = roleCheck != null;
        if (!isNotMatchedAUserRoleClaimWithRequestRoles)
        {
            string message = "You are not authorized.";
            LogDetail value = _logDetail(request, next, message);
            _logger.Information(JsonConvert.SerializeObject(value));
            throw new AuthorizationException(message);
        }

        TResponse response = await next();
        return response;
    }

    private LogDetail _logDetail(TRequest request, RequestHandlerDelegate<TResponse> next,string message)
    {
        List<LogParameter> parameters = new List<LogParameter>(1)
        {
            new LogParameter
            {
                
                Type = request.GetType().Name,
                Value = request
            }
        };
        LogDetail value = new LogDetail
        {
            FullName= message,
            MethodName = request.GetType().Name,
            Parameters = parameters,
            User = userClientIp(_httpContextAccessor.HttpContext)
        };
        return value;
    }

    private static string userClientIp(HttpContext context)
    {
        var userClaimsIpAddress = (context != null && context.User?.Claims?.Count() > 0) ?
                      context.User.Claims.FirstOrDefault(c => c.Type == "LoginIpAddress")?.Value :
                      context.Request.Headers.ContainsKey("X-Client-IP") ?
                      context.Request.Headers["X-Client-IP"].ToString() :
                      !string.IsNullOrEmpty(Convert.ToString(context.Connection.RemoteIpAddress)) ?
                      context.Connection.RemoteIpAddress?.ToString() : "?";
        return userClaimsIpAddress;
    }
}
