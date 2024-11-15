using Application.Repositories;
using Application.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Examples;
using Persistence.Repositories.Users;

namespace Persistence;

public sealed class UnitOfWorkAsync : IUnitOfWorkAsync
{
    private readonly DbContext _context;
    public UnitOfWorkAsync(BaseDbContext context) => _context = context;

    private UserRepositoryAsync _userRepository;
    private RoleRepositoryAsync _roleRepository;
    private UserRoleRepositoryAsync _userRoleRepository;
    private RefreshTokenRepositoryAsync _refreshTokenRepository;
    private ProductRepositoryAsync _productRepository;
    private ExampleEntityRepositoryAsync _exampleEntityRepository;

    public IUserDalAsync UserRepository => _userRepository ??= new UserRepositoryAsync(_context);
    public IRoleDalAsync RoleRepository => _roleRepository ??= new RoleRepositoryAsync(_context);
    public IUserRoleDalAsync UserRoleRepository => _userRoleRepository ??= new UserRoleRepositoryAsync(_context);
    public IRefreshTokenDalAsync RefreshTokenRepository => _refreshTokenRepository ??= new RefreshTokenRepositoryAsync(_context);
    public IProductDalAsync ProductRepository => _productRepository ??= new ProductRepositoryAsync(_context);
    public IExampleEntityDalAsync ExampleEntityRepository => _exampleEntityRepository ??= new ExampleEntityRepositoryAsync(_context);

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}
