using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations.Users;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles").HasKey(uoc => uoc.Id);

        builder.Property(uoc => uoc.Id).HasColumnName("Id").IsRequired();
        builder.Property(uoc => uoc.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(uoc => uoc.RoleId).HasColumnName("RoleId").IsRequired();
        builder.Property(uoc => uoc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(uoc => uoc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(uoc => uoc.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(x => x.IsDelete).HasDefaultValue(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.OrderNo).HasDefaultValue(1);
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        //builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");
        builder.HasOne(uoc => uoc.User);
        builder.HasOne(uoc => uoc.Role);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    private IEnumerable<UserRole> _seeds
    {
        get
        {
            yield return new()
            {
                Id = Guid.NewGuid(),
                UserId = UserConfiguration.AdminId,
                RoleId = RoleConfiguration.AdminId
            };
        }
    }
}