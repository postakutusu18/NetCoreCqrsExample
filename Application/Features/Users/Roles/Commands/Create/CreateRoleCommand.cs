using Core.Application.Pipelines.Authorization;
using MediatR;
using Core.Application.Results;
using Application.Features.UserFeatures.Roles.Constants;
using Application.Features.UserFeatures.Roles.Commands.Create;
public class CreateRoleCommand : IRequest<IDataResult<CreatedRoleResponse>>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { RoleOperationClaims.CreateRole }; // Admin, Write,

    public CreateRoleCommand()
    {
        Name = string.Empty;
    }

    public CreateRoleCommand(string name)
    {
        Name = name;
    }


   
}
