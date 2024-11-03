using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IUserRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid> { }
