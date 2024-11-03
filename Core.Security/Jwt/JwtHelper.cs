using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.Security.Jwt;

public class JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId> : ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>
{
    private readonly TokenOptions _tokenOptions;

    public JwtHelper(TokenOptions tokenOptions)
    {
        _tokenOptions = tokenOptions;
    }

    public virtual AccessToken CreateToken(UserBase<TUserId> user, IList<RoleBase<TOperationClaimId>> operationClaims)
    {
        DateTime accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(
            _tokenOptions,
            user,
            signingCredentials,
            operationClaims,
            accessTokenExpiration
        );
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken() { Token = token, ExpirationDate = accessTokenExpiration };
    }

    public RefreshTokenBase<TRefreshTokenId, TUserId> CreateRefreshToken(UserBase<TUserId> user, string ipAddress)
    {
        return new RefreshTokenBase<TRefreshTokenId, TUserId>()
        {
            UserId = user.Id,
            Token = randomRefreshToken(),
            ExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            CreatedByIp = ipAddress
        };
    }

    public virtual JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        UserBase<TUserId> user,
        SigningCredentials signingCredentials,
        IList<RoleBase<TOperationClaimId>> operationClaims,
        DateTime accessTokenExpiration
    )
    {
        return new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
    }

    protected virtual IEnumerable<Claim> SetClaims(UserBase<TUserId> user, IList<RoleBase<TOperationClaimId>> operationClaims)
    {
        List<Claim> claims = [];
        claims.AddNameIdentifier(user!.Id!.ToString()!);
        claims.AddEmail(user.Email);
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims.ToImmutableList();
    }

    private string randomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
}
