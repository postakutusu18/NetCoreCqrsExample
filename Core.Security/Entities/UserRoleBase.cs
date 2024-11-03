using Core.Persistance.Repositories;

namespace Core.Security.Entities;

public class UserRoleBase<TId, TUserId, TRoleId> : Entity<TId>
{
    public TUserId UserId { get; set; }
    public TRoleId RoleId { get; set; }

    public UserRoleBase()
    {
        UserId = default!;
        RoleId = default!;
    }

    public UserRoleBase(TUserId userId, TRoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public UserRoleBase(TId id, TUserId userId, TRoleId roleId)
        : base(id)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
