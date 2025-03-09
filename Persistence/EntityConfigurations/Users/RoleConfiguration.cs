using Application.Features.UserFeatures.Auth.Constants;
using Application.Features.UserFeatures.Roles.Constants;
using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.Users.Constants;
using Core.Security.Constants;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.EntityConfigurations.Users;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(x => x.IsDelete).HasDefaultValue(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.OrderNo).HasDefaultValue(1);
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        //builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        builder.HasQueryFilter(x => x.IsDelete == false);
        builder.HasIndex(x => x.IsDelete).HasFilter("IsDelete = 0");

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<Role> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<Role> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (Role claim in featureOperationClaims)
                yield return claim;
        }
    }

    private IEnumerable<Role> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<Role> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.AdminRole },
                new() { Id = ++lastId, Name = AuthOperationClaims.ReadRole },
                new() { Id = ++lastId, Name = AuthOperationClaims.WriteRole },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeTokenRole },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = RoleOperationClaims.AdminRole  },
                new() { Id = ++lastId, Name = RoleOperationClaims.ReadRole },
                new() { Id = ++lastId, Name = RoleOperationClaims.WriteRole },
                new() { Id = ++lastId, Name = RoleOperationClaims.CreateRole },
                new() { Id = ++lastId, Name = RoleOperationClaims.UpdateRole },
                new() { Id = ++lastId, Name = RoleOperationClaims.DeleteRole },
            ]
        );
        #endregion

        #region UserRoles
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserRoleOperationClaims.AdminRole },
                new() { Id = ++lastId, Name = UserRoleOperationClaims.ReadRole },
                new() { Id = ++lastId, Name = UserRoleOperationClaims.WriteRole },
                new() { Id = ++lastId, Name = UserRoleOperationClaims.CreateRole },
                new() { Id = ++lastId, Name = UserRoleOperationClaims.UpdateRole },
                new() { Id = ++lastId, Name = UserRoleOperationClaims.DeleteRole },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.AdminRole },
                new() { Id = ++lastId, Name = UsersOperationClaims.ReadRole },
                new() { Id = ++lastId, Name = UsersOperationClaims.WriteRole },
                new() { Id = ++lastId, Name = UsersOperationClaims.CreateRole },
                new() { Id = ++lastId, Name = UsersOperationClaims.UpdateRole },
                new() { Id = ++lastId, Name = UsersOperationClaims.DeleteRole },
            ]
        );
        #endregion

        return featureOperationClaims;
    }
}