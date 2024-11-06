using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Context;

public sealed class OracleDbContext : BaseDbContext
{
    public OracleDbContext(DbContextOptions<OracleDbContext> options, IConfiguration configuration)
        : base(options, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder.UseOracle(Configuration.GetConnectionString("OracleConnect")));
        }
    }
}