using Core.Persistance.Repositories;

namespace Core.Security.Entities;

public class RefreshTokenBase<TId, TUserId> : Entity<TId>
{
    public TUserId UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? ReasonRevoked { get; set; }

    public RefreshTokenBase()
    {
        UserId = default!;
        Token = string.Empty;
        CreatedByIp = string.Empty;
    }

    public RefreshTokenBase(TUserId userId, string token, DateTime expirationDate, string createdByIp)
    {
        UserId = userId;
        Token = token;
        ExpirationDate = expirationDate;
        CreatedByIp = createdByIp;
    }

    public RefreshTokenBase(TId id, TUserId userId, string token, DateTime expirationDate, string createdByIp)
        : base(id)
    {
        UserId = userId;
        Token = token;
        ExpirationDate = expirationDate;
        CreatedByIp = createdByIp;
    }
}
