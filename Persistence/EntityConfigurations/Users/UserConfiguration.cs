using Core.Security.Enums;
using Core.Security.Hashing;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(x => x.IsDelete).HasDefaultValue(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.OrderNo).HasDefaultValue(1);
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        //builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");
        builder.HasMany(u => u.UserRoles);
        builder.HasMany(u => u.RefreshTokens);
        //builder.HasMany(u => u.EmailAuthenticators);
        //builder.HasMany(u => u.OtpAuthenticators);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "Passw0rd!",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    Id = AdminId,
                    Email = "postakutusu18@hotmail.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsActive = true,
                    IsDelete = false,
                    FirstName ="Selçuk",
                    LastName ="KARAAĞAÇ",
                    AuthenticatorType = AuthenticatorType.Email
                };
            yield return adminUser;
        }
    }
}