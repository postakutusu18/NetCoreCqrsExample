using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class RoleRepositoryAsync : EfAsyncRepositoryBase<Role, int>, IRoleDalAsync
{
    public RoleRepositoryAsync(DbContext context)
        : base(context) { }
}
