using Core.Application.Responses;

namespace Application.Features.UserRoles.Commands.Update;

public class UpdatedUserRoleResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
