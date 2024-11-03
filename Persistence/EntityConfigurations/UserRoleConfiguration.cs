using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations;

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

        builder.HasQueryFilter(uoc => !uoc.DeletedDate.HasValue);

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