using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Users;

public class RoleRepositoryAsync : EfAsyncRepositoryBase<Role, int>, IRoleDalAsync
{
    public RoleRepositoryAsync(DbContext context)
        : base(context) { }
}
