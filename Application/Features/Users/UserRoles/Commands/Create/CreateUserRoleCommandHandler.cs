using Application.Features.UserFeatures.UserRoles.Rules;
using Application.Repositories;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserFeatures.UserRoles.Commands.Create;
public class CreateUserRoleCommandHandler
       : IRequestHandler<CreateUserRoleCommand, IDataResult<CreatedUserRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly UserRoleRules _userRoleRules;

    public CreateUserRoleCommandHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
        UserRoleRules userRoleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userRoleRules = userRoleRules;
    }

    public async Task<IDataResult<CreatedUserRoleResponse>> Handle(
        CreateUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        await _userRoleRules.UserShouldNotHasRoleAlreadyWhenInsert(
            request.UserId,
            request.RoleId
        );
        UserRole mappedUserRole = request.Adapt<UserRole>();

        UserRole createdUserRole = await _unitOfWorkAsync.UserRoleRepository.AddAsync(mappedUserRole);
        await _unitOfWorkAsync.SaveAsync();

        var createdUserRoleDto = createdUserRole.Adapt<CreatedUserRoleResponse>();
        var result = new SuccessDataResult<CreatedUserRoleResponse>(createdUserRoleDto);
        return result;
    }
}