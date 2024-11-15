using Application.Repositories;
using Application.Repositories.Users;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistance.Paging;
using Domains.Users;
using Mapster;
using MediatR;
namespace Application.Features.UserFeatures.Roles.Queries.GetList;

public class GetListRoleQueryHandler
        : IRequestHandler<GetListRoleQuery, IDataResult<GetListResponse<GetListRoleResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;

    public GetListRoleQueryHandler(IUnitOfWorkAsync unitOfWorkAsync)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
    }

    public async Task<IDataResult<GetListResponse<GetListRoleResponse>>> Handle(
        GetListRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        IPaginate<Role> roles = await _unitOfWorkAsync.RoleRepository.GetListAsync(
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