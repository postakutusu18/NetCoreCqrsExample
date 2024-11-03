using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class UserRoleRepository
    : EfRepositoryBase<UserRole, Guid, BaseDbContext>,
        IUserRoleRepository
{
    public UserRoleRepository(BaseDbContext context)
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
