using Core.Application.Pipelines.Authorization;
using MediatR;
using Application.Features.Roles.Constants;
using Application.Features.Roles.Commands.Create;
using Core.Application.Results;
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
