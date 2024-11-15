using Application.Features.UserFeatures.UserRoles.Commands.Update;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.UserRoles.Commands.Update;


public partial class UpdateUserRoleCommand
{
    public class UpdateUserRoleCommandHandler
        : IRequestHandler<UpdateUserRoleCommand,IDataResult<UpdatedUserRoleResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRoleRules _userRoleRules;

        public UpdateUserRoleCommandHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
            UserRoleRules userRoleRules
        )
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _userRoleRules = userRoleRules;
        }

        public async Task<IDataResult<UpdatedUserRoleResponse>> Handle(
            UpdateUserRoleCommand request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _unitOfWorkAsync.UserRoleRepository.GetAsync(
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

            UserRole updatedUserRole = await _unitOfWorkAsync.UserRoleRepository.UpdateAsync(
                mappedUserRole
            );
            await _unitOfWorkAsync.SaveAsync();

            var updatedUserRoleDto = updatedUserRole.Adapt<UpdatedUserRoleResponse>();
            var result = new SuccessDataResult<UpdatedUserRoleResponse>(updatedUserRoleDto);
            return result;
        }
    }
}