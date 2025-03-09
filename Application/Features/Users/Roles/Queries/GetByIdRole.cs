using Application.Features.UserFeatures.Roles.Constants;
using Application.Features.UserFeatures.Roles.Rules;

namespace Application.Features.Users.Roles.Queries;

public class GetByIdRole : IRequestHandler<GetByIdRoleQuery, IDataResult<GetByIdRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;
    private readonly ILocalizationService _localizationService;

    public GetByIdRole(
        IUnitOfWorkAsync unitOfWorkAsync,
        RoleRules roleRules,
        ILocalizationService localizationService
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetByIdRoleResponse>> Handle(
        GetByIdRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _unitOfWorkAsync.RoleRepository.GetAsync(
            predicate: b => b.Id == request.Id,
            cancellationToken: cancellationToken,
            enableTracking: false
        );
        await _roleRules.RoleShouldExistWhenSelected(role);

        string message = await _localizationService.GetLocalizedAsync(RoleMessages.SuccessRecord, RoleMessages.SectionName);
        GetByIdRoleResponse response = role.Adapt<GetByIdRoleResponse>();
        var result = new  SuccessDataResult<GetByIdRoleResponse>(response, message);
        return result;
    }
}

public record GetByIdRoleQuery(int Id) : IRequest<IDataResult<GetByIdRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => [RoleOperationClaims.ReadRole];
}
public record GetByIdRoleResponse(int Id, string Name);

