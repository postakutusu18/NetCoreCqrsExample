using Application.Features.UserFeatures.Auth.Commands.RefreshTokens;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshTokens;

public partial class RefreshTokenCommand : IRequest<RefreshedTokensResponse>
{
    public string RefreshToken { get; set; }
    public string IpAddress { get; set; }

    public RefreshTokenCommand()
    {
        RefreshToken = string.Empty;
        IpAddress = string.Empty;
    }

    public RefreshTokenCommand(string refreshToken, string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }
}