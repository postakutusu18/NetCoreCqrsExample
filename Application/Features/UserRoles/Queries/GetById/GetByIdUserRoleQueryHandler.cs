using Application.Features.UserRoles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserRoles.Queries.GetById;

public partial class GetByIdUserRoleQuery
{
    public class GetByIdUserRoleQueryHandler
        : IRequestHandler<GetByIdUserRoleQuery,IDataResult<GetByIdUserRoleResponse>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly UserRoleRules _userRoleRules;

        public GetByIdUserRoleQueryHandler(
            IUserRoleRepository userRoleRepository,
            UserRoleRules userRoleRules
        )
        {
            _userRoleRepository = userRoleRepository;
            _userRoleRules = userRoleRules;
        }

        public async Task<IDataResult<GetByIdUserRoleResponse>> Handle(
            GetByIdUserRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _userRoleRepository.GetAsync(
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

