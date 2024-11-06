using Core.Persistance.DbHelper;
using Core.Persistance.Repositories;
using Domains;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Context;

public class BaseDbContext : DbContext
{

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
     : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected IConfiguration Configuration { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> OperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.NameUpper(DataBaseEnums.MsSql);
        //modelBuilder.MappingInfo();//mapingler yerine aşağıdaki kod kullanılabilir.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // ***Veri Tabanı Ayarları
        //nameupper fonksiyonu içine ilgili veritabaı tipini gönderiniz.
        //DataAccess> BaseConfiguration dosyasını güncelleyiniz.
        //Business > BusinessModule dosyasını güncelleyiniz.
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreConnect"))
            .EnableSensitiveDataLogging());
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker
            .Entries<Entity<object>>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            _ = entry.State switch
            {
                EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow,
                _ => entry.Entity.UpdatedDate
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
