using Core.Application.Responses;

namespace Application.Features.UserRoles.Queries.GetById;

public class GetByIdUserRoleResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
