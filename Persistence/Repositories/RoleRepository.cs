using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Persistence.Context;

namespace Persistence.Repositories;

public class RoleRepository : EfRepositoryBase<Role, int, BaseDbContext>, IRoleRepository
{
    public RoleRepository(BaseDbContext context)
        : base(context) { }
}
