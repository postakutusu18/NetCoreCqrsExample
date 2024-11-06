using Application.Repositories;
using Application.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(
        //    options => options.UseSqlServer(configuration.GetConnectionString("NetCoreConnectionString"))
        //);

        //services.AddDbContext<BaseDbContext>(ServiceLifetime.Transient);//Postgre
        services.AddDbContext<BaseDbContext, MsSqlDbContext>(ServiceLifetime.Transient);//MsSQL


        //services.AddScoped<IProductDalAsync, ProductRepositoryAsync>();
        //services.AddScoped<IRoleDalAsync, RoleRepositoryAsync>();
        //services.AddScoped<IRefreshTokenDalAsync, RefreshTokenRepositoryAsync>();
        //services.AddScoped<IUserDalAsync, UserRepositoryAsync>();
        //services.AddScoped<IUserRoleDalAsync, UserRoleRepositoryAsync>();
        services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
        return services;
    }
}