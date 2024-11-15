using Application.Features.UserFeatures.Auth.Commands.RevokeToken;
using Application.Features.UserFeatures.Auth.Constants;

namespace Application.Features.Auth.Commands.RevokeToken;

public partial class RevokeTokenCommand : IRequest<RevokedTokenResponse>, ISecuredRequest
{
    public string Token { get; set; }
    public string IpAddress { get; set; }

    public string[] Roles => [AuthOperationClaims.AdminRole, AuthOperationClaims.RevokeTokenRole];

    public RevokeTokenCommand()
    {
        Token = string.Empty;
        IpAddress = string.Empty;
    }

    public RevokeTokenCommand(string token, string ipAddress)
    {
        Token = token;
        IpAddress = ipAddress;
    }
}