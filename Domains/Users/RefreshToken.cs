namespace Domains.Users;

public class RefreshToken : RefreshTokenBase<Guid, Guid>
{
    public virtual User User { get; set; } = default!;
}
