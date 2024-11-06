using Core.Persistance.DbHelper;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations;

public class ProductConfiguration : BaseConfiguration<Product,Guid>
{
    public override void ConfigureEntity(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Products_Name").IsUnique();
        builder.Property(o => o.Price).HasPrecision(18, 4);

        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");
        //Product[] productSeeds = { new(id: 1, name: "BMW"), new(id: 2, name: "Mercedes") };
        //builder.HasData(productSeeds);
    }
}
