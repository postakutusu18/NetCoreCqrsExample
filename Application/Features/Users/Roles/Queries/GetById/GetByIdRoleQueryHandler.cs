using Application.Features.UserFeatures.Roles.Rules;

namespace Application.Features.UserFeatures.Roles.Queries.GetById;

public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, IDataResult<GetByIdRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;

    public GetByIdRoleQueryHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
        RoleRules roleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
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

        GetByIdRoleResponse response = role.Adapt<GetByIdRoleResponse>();
        var result = new SuccessDataResult<GetByIdRoleResponse>(response);
        return result;
    }
}