using Core.Application.Responses;

namespace Application.Features.UserRoles.Commands.Delete;

public class DeletedUserRoleResponse : IResponse
{
    public Guid Id { get; set; }
}
