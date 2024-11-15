using Application.Repositories.Users;
using Core.Persistance.Repositories;
using Domains.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Users;

public class UserRepositoryAsync : EfAsyncRepositoryBase<User, Guid>, IUserDalAsync
{
    public UserRepositoryAsync(DbContext context)
        : base(context) { }
}
