using Core.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistance.DbHelper;

public abstract class BaseConfiguration<TEntity,TId> : IEntityTypeConfiguration<TEntity>
  where TEntity : Entity<TId>
{
    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntity(builder);
        builder.Property(x => x.IsDelete).HasDefaultValue(false);
        builder.Property(x => x.OrderNo).HasDefaultValue(1);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        //builder.Property(x => x.LanguageId).HasDefaultValue(1);

        //Postgre Sql için Kullanın
        //builder.Property(x => x.CreateTime).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        //builder.Property(x => x.UpdateTime).HasDefaultValueSql("now()").ValueGeneratedOnAdd();

        //MsSql için Kullanın
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
    }

    
}