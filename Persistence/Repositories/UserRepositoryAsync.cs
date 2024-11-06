using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class UserRepositoryAsync : EfAsyncRepositoryBase<User, Guid>, IUserDalAsync
{
    public UserRepositoryAsync(DbContext context)
        : base(context) { }
}
