using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IUserRoleRepository : IAsyncRepository<UserRole, Guid>, IRepository<UserRole, Guid>
{
    Task<IList<Role>> GetOperationClaimsByUserIdAsync(Guid userId);
}
