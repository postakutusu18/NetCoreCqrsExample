namespace Domains.Users;

public class User : UserBase<Guid>
{
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
}
