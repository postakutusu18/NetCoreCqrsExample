using Core.Application.Dtos;

namespace Application.Features.UserRoles.Queries.GetList;

public class GetListUserRoleListResponse : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
