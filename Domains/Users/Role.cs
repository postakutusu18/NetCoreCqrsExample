
namespace Domains.Users;

public class Role : RoleBase<int>
{
}
public class UserRole : UserRoleBase<Guid, Guid, int>
{
    public virtual User User { get; set; } = default!;
    public virtual Role Role { get; set; } = default!;
}
