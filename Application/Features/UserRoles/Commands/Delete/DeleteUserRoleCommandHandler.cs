using Application.Features.UserRoles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserRoles.Commands.Delete;

public class DeleteUserRoleCommandHandler
     : IRequestHandler<DeleteUserRoleCommand,IDataResult<DeletedUserRoleResponse>>
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly UserRoleRules _userRoleRules;

    public DeleteUserRoleCommandHandler(
        IUserRoleRepository userRoleRepository,
        UserRoleRules userRoleRules
    )
    {
        _userRoleRepository = userRoleRepository;
        _userRoleRules = userRoleRules;
    }

    public async Task<IDataResult<DeletedUserRoleResponse>> Handle(
        DeleteUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        UserRole? userRole = await _userRoleRepository.GetAsync(
            predicate: uoc => uoc.Id.Equals(request.Id),
            cancellationToken: cancellationToken
        );
        await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);

        await _userRoleRepository.DeleteAsync(userRole!);

        DeletedUserRoleResponse response = userRole.Adapt<DeletedUserRoleResponse>();
        var result = new SuccessDataResult<DeletedUserRoleResponse>(response);
        return result;
    }
}