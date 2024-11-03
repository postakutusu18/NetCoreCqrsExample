using Core.Security.Entities;

namespace Core.Security.Jwt;

public interface ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>
{
    public AccessToken CreateToken(UserBase<TUserId> user, IList<RoleBase<TOperationClaimId>> operationClaims);

    public RefreshTokenBase<TRefreshTokenId, TUserId> CreateRefreshToken(UserBase<TUserId> user, string ipAddress);
}
