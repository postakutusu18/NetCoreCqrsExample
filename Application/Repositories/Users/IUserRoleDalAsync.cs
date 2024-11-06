using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IUserRoleDalAsync : IAsyncRepository<UserRole, Guid>
{
    Task<IList<Role>> GetOperationClaimsByUserIdAsync(Guid userId);
}
