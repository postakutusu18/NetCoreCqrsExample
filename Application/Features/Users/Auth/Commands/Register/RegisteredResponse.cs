using Core.Application.Responses;
using Core.Security.Jwt;
using Domains.Users;

namespace Application.Features.UserFeatures.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }

    public RegisteredResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RegisteredResponse(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
