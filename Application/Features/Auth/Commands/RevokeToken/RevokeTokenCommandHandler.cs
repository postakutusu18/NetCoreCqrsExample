using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Auth.Commands.RevokeToken;

public partial class RevokeTokenCommand
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules)
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
}