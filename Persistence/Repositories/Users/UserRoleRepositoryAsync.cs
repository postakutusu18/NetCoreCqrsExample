using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Users;

public class UserRoleRepositoryAsync
    : EfAsyncRepositoryBase<UserRole, Guid>,
        IUserRoleDalAsync
{
    public UserRoleRepositoryAsync(DbContext context)
        : base(context) { }

    public async Task<IList<Role>> GetOperationClaimsByUserIdAsync(Guid userId)
    {
        List<Role> operationClaims = await Query()
            .AsNoTracking()
            .Where(p => p.UserId.Equals(userId))
            .Select(p => new Role { Id = p.RoleId, Name = p.Role.Name })
            .ToListAsync();
        return operationClaims;
    }
}
