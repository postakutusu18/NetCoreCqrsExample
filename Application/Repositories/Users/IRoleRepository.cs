using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IRoleRepository : IAsyncRepository<Role, int>, IRepository<Role, int> { }
