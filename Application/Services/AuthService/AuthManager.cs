using Application.Repositories.Users;
using Core.Security.Entities;
using Core.Security.Jwt;
using Domains.Users;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper<Guid, int, Guid> _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly IUserRoleRepository _userRoleRepository;

    public AuthManager(
        IUserRoleRepository userRoleRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenHelper<Guid, int, Guid> tokenHelper,
        IConfiguration configuration
    )
    {
        _userRoleRepository = userRoleRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;

        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IList<Role> operationClaims = await _userRoleRepository.GetOperationClaimsByUserIdAsync(user.Id);
        AccessToken accessToken = _tokenHelper.CreateToken(
            user,
            operationClaims.Select(op => (RoleBase<int>)op).ToImmutableList()
        );
        return accessToken;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(Guid userId)
    {
        List<RefreshToken> refreshTokens = await _refreshTokenRepository.GetOldRefreshTokensAsync(
            userId,
            _tokenOptions.RefreshTokenTTL
        );
        await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
    }

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }

    public async Task RevokeRefreshToken(
        RefreshToken refreshToken,
        string ipAddress,
        string? reason = null,
        string? replacedByToken = null
    )
    {
        refreshToken.RevokedDate = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
      RefreshTokenBase<Guid, Guid> newCoreRefreshToken = _tokenHelper.CreateRefreshToken(
            user,
            ipAddress
        );
        RefreshToken newRefreshToken = newCoreRefreshToken.Adapt<RefreshToken>();
        await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
    {
        RefreshToken? childToken = await _refreshTokenRepository.GetAsync(predicate: r =>
            r.Token == refreshToken.ReplacedByToken
        );

        if (childToken?.RevokedDate != null && childToken.ExpirationDate <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else
            await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshTokenBase<Guid, Guid> coreRefreshToken = _tokenHelper.CreateRefreshToken(
            user,
            ipAddress
        );
        RefreshToken refreshToken = coreRefreshToken.Adapt<RefreshToken>();
        return Task.FromResult(refreshToken);
    }
}