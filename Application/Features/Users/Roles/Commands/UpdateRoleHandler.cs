using Application.Features.UserFeatures.Roles.Constants;
using Application.Features.UserFeatures.Roles.Rules;

namespace Application.Features.Users.Roles.Commands;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, IDataResult<UpdatedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;
    private readonly ILocalizationService _localizationService;

    public UpdateRoleHandler(
    IUnitOfWorkAsync unitOfWorkAsync,
    RoleRules roleRules
,
    ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<UpdatedRoleResponse>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _unitOfWorkAsync.RoleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);
        await _roleRules.RoleNameShouldNotExistWhenUpdating(request.Id, request.Name);
        var mappedRole = request.Adapt(role);

        Role updatedRole = await _unitOfWorkAsync.RoleRepository.UpdateAsync(mappedRole);
        await _unitOfWorkAsync.SaveAsync();

        UpdatedRoleResponse response = updatedRole.Adapt<UpdatedRoleResponse>();
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.SuccessUpdated, RoleMessages.SectionName);
        var result = new SuccessDataResult<UpdatedRoleResponse>(response,message);
        return result;
    }
}

public record UpdateRoleCommand(int Id, string Name) : IRequest<IDataResult<UpdatedRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.AdminRole, RoleOperationClaims.WriteRole, RoleOperationClaims.UpdateRole };
}
public record UpdatedRoleResponse(int Id, string Name);
