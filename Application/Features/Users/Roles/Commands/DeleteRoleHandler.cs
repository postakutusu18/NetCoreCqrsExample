using Application.Features.UserFeatures.Roles.Rules;
using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Commands;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, IDataResult<DeletedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;
    private readonly ILocalizationService _localizationService;

    public DeleteRoleHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
        RoleRules roleRules
,
        ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<DeletedRoleResponse>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _unitOfWorkAsync.RoleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);

        await _unitOfWorkAsync.RoleRepository.DeleteAsync(entity: role!);
        await _unitOfWorkAsync.SaveAsync();

        DeletedRoleResponse response = role.Adapt<DeletedRoleResponse>();
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.SuccessDeleted, RoleMessages.SectionName);
        var result = new SuccessDataResult<DeletedRoleResponse>(response,message);
        return result;
    }
}
public record DeleteRoleCommand(int Id) : IRequest<IDataResult<DeletedRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.AdminRole, RoleOperationClaims.WriteRole, RoleOperationClaims.DeleteRole };
}
public record DeletedRoleResponse(int Id) : IResponse;
