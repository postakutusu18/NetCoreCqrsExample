using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Context;

public sealed class MySqlDbContext : BaseDbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration)
        : base(options, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder.UseMySQL(Configuration.GetConnectionString("MySqlConnect")));
        }
    }
}
