using Application.Features.UserRoles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserRoles.Commands.Create;
public class CreateUserRoleCommandHandler
       : IRequestHandler<CreateUserRoleCommand, IDataResult<CreatedUserRoleResponse>>
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly UserRoleRules _userRoleRules;

    public CreateUserRoleCommandHandler(
        IUserRoleRepository userRoleRepository,
        UserRoleRules userRoleRules
    )
    {
        _userRoleRepository = userRoleRepository;
        _userRoleRules = userRoleRules;
    }

    public async Task< IDataResult<CreatedUserRoleResponse>> Handle(
        CreateUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        await _userRoleRules.UserShouldNotHasRoleAlreadyWhenInsert(
            request.UserId,
            request.RoleId
        );
        UserRole mappedUserRole = request.Adapt<UserRole>();

        UserRole createdUserRole = await _userRoleRepository.AddAsync(mappedUserRole);

        var createdUserRoleDto = createdUserRole.Adapt<CreatedUserRoleResponse>();
        var result = new SuccessDataResult<CreatedUserRoleResponse>( createdUserRoleDto);
        return result;
    }
}