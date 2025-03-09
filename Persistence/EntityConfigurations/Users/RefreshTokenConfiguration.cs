using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations.Users;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens").HasKey(rt => rt.Id);

        builder.Property(rt => rt.Id).HasColumnName("Id").IsRequired();
        builder.Property(rt => rt.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(rt => rt.Token).HasColumnName("Token").IsRequired();
        builder.Property(rt => rt.ExpirationDate).HasColumnName("ExpiresDate").IsRequired();
        builder.Property(rt => rt.CreatedByIp).HasColumnName("CreatedByIp").IsRequired();
        builder.Property(rt => rt.RevokedDate).HasColumnName("RevokedDate");
        builder.Property(rt => rt.RevokedByIp).HasColumnName("RevokedByIp");
        builder.Property(rt => rt.ReplacedByToken).HasColumnName("ReplacedByToken");
        builder.Property(rt => rt.ReasonRevoked).HasColumnName("ReasonRevoked");
        builder.Property(rt => rt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(rt => rt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(rt => rt.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(x => x.IsDelete).HasDefaultValue(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.OrderNo).HasDefaultValue(1);
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        //builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");

        builder.HasOne(rt => rt.User);

        builder.HasBaseType((string)null!);
    }
}
