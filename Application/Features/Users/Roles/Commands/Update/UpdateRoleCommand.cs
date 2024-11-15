using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.UserFeatures.Roles.Commands.Update;

public class UpdateRoleCommand : IRequest<IDataResult<UpdatedRoleResponse>>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdateRoleCommand()
    {
        Name = string.Empty;
    }

    public UpdateRoleCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string[] Roles => new[] { RoleOperationClaims.AdminRole, RoleOperationClaims.WriteRole, RoleOperationClaims.UpdateRole };


}
