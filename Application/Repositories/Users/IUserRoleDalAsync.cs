
namespace Application.Repositories.Users;

public interface IUserRoleDalAsync : IAsyncRepository<UserRole, Guid>
{
    Task<IList<Role>> GetOperationClaimsByUserIdAsync(Guid userId);
}
