using Core.Security.Jwt;

namespace Application.Features.UserFeatures.Auth.Commands.RefreshTokens;

public class RefreshedTokensResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }

    public RefreshedTokensResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RefreshedTokensResponse(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
