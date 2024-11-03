using Application.Features.UserRoles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserRoles.Commands.Update;


public partial class UpdateUserRoleCommand
{
    public class UpdateUserRoleCommandHandler
        : IRequestHandler<UpdateUserRoleCommand,IDataResult<UpdatedUserRoleResponse>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly UserRoleRules _userRoleRules;

        public UpdateUserRoleCommandHandler(
            IUserRoleRepository userRoleRepository,
            UserRoleRules userRoleRules
        )
        {
            _userRoleRepository = userRoleRepository;
            _userRoleRules = userRoleRules;
        }

        public async Task<IDataResult<UpdatedUserRoleResponse>> Handle(
            UpdateUserRoleCommand request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _userRoleRepository.GetAsync(
                predicate: uoc => uoc.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);
            await _userRoleRules.UserShouldNotHasRoleAlreadyWhenUpdated(
                request.Id,
                request.UserId,
                request.RoleId
            );
            UserRole mappedUserRole = request.Adapt<UserRole>();

            UserRole updatedUserRole = await _userRoleRepository.UpdateAsync(
                mappedUserRole
            );

            var updatedUserRoleDto = updatedUserRole.Adapt<UpdatedUserRoleResponse>();
            var result = new SuccessDataResult<UpdatedUserRoleResponse>(updatedUserRoleDto);
            return result;
        }
    }
}