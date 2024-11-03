using Application.Repositories.Users;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistance.Paging;
using Domains.Users;
using Mapster;
using MediatR;
namespace Application.Features.Roles.Queries.GetList;

public class GetListRoleQueryHandler
        : IRequestHandler<GetListRoleQuery,IDataResult<GetListResponse<GetListRoleResponse>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetListRoleQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IDataResult<GetListResponse<GetListRoleResponse>>> Handle(
        GetListRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        IPaginate<Role> roles = await _roleRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        var response = roles.Adapt<GetListResponse<GetListRoleResponse>>();
        var result = new SuccessDataResult<GetListResponse<GetListRoleResponse>>(response);
        return result;
    }
}