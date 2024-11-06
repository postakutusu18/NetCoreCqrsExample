using Application.Repositories.Users;

namespace Application.Repositories;

public interface IUnitOfWorkAsync : IAsyncDisposable
{
    //ILogDalAsync LogDalAsync { get; }
    IUserDalAsync UserRepository { get; }
    IRoleDalAsync RoleRepository { get; }
    //ISubRoleDalAsync SubRoleDalAsync { get; }
    IUserRoleDalAsync UserRoleRepository { get; }
    IRefreshTokenDalAsync RefreshTokenRepository { get; }
    //IAdminMenuDalAsync AdminMenuDalAsync { get; }
    //IAdminMenuRoleDalAsync AdminMenuRoleDalAsync { get; }
    //IBlackListIpDalAsync BlackListIpDalAsync { get; }
    IProductDalAsync ProductRepository { get; }
    Task<int> SaveAsync();
}