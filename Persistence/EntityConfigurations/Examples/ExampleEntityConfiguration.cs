using Core.Persistance.DbHelper;
using Domains.Examples;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations.Examples;

public class ExampleEntityConfiguration : BaseConfiguration<ExampleEntity, Guid>
{
    public override void ConfigureEntity(EntityTypeBuilder<ExampleEntity> builder)
    {
        builder.ToTable("ExampleEntities").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedNever().HasDefaultValue(NewId.NextSequentialGuid()).ValueGeneratedOnAdd();
        
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_ExampleEntities_Name").IsUnique();

        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");
        
    }
}
