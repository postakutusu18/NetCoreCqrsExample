using Application.Features.UserFeatures.Roles.Constants;
using Application.Features.UserFeatures.Roles.Rules;

namespace Application.Features.Users.Roles.Commands;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, IDataResult<CreatedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;

    public CreateRoleHandler(
       IUnitOfWorkAsync unitOfWorkAsync,
        RoleRules roleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<CreatedRoleResponse>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        await _roleRules.RoleNameShouldNotExistWhenCreating(request.Name);
        Role mappedRole = request.Adapt<Role>();

        Role createdRole = await _unitOfWorkAsync.RoleRepository.AddAsync(mappedRole);
        await _unitOfWorkAsync.SaveAsync();

        CreatedRoleResponse response = createdRole.Adapt<CreatedRoleResponse>();
        var result = new SuccessDataResult<CreatedRoleResponse>(response);
        return result;
    }
}

public record CreateRoleCommand(string Name) : IRequest<IDataResult<CreatedRoleResponse>>, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.CreateRole };
}
public record CreatedRoleResponse(int Id, string Name);
