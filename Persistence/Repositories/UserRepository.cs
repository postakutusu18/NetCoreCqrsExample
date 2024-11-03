using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Persistence.Context;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, Guid, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
