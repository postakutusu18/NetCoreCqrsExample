using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IUserDalAsync : IAsyncRepository<User, Guid>{ }
