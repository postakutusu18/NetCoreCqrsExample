﻿using Application.Features.UserFeatures.UserRoles.Queries.GetById;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.UserRoles.Queries.GetById;

public partial class GetByIdUserRoleQuery
{
    public class GetByIdUserRoleQueryHandler
        : IRequestHandler<GetByIdUserRoleQuery,IDataResult<GetByIdUserRoleResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRoleRules _userRoleRules;

        public GetByIdUserRoleQueryHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
            UserRoleRules userRoleRules
        )
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _userRoleRules = userRoleRules;
        }

        public async Task<IDataResult<GetByIdUserRoleResponse>> Handle(
            GetByIdUserRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _unitOfWorkAsync.UserRoleRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);

            GetByIdUserRoleResponse userRoleDto = userRole.Adapt<GetByIdUserRoleResponse>();
            var result = new SuccessDataResult<GetByIdUserRoleResponse>(userRoleDto);
            return result;
        }
    }
}

