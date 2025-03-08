using Application.Features.UserFeatures.Auth.Constants;
using Application.Features.UserFeatures.Auth.Rules;
using Application.Services.AuthService;

namespace Application.Features.Users.Auth.Commands;

public class RevokeTokenProcess : IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
{
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;

    public RevokeTokenProcess(IAuthService authService, AuthBusinessRules authBusinessRules)
    {
        _authService = authService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RevokedTokenResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
        await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
        await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

        await _authService.RevokeRefreshToken(token: refreshToken!, request.IpAddress, reason: "Revoked without replacement");

        RevokedTokenResponse revokedTokenResponse = refreshToken.Adapt<RevokedTokenResponse>();
        return revokedTokenResponse;
    }
}
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
public class RevokedTokenResponse : IResponse
{
    public Guid Id { get; set; }
    public string Token { get; set; }

}
