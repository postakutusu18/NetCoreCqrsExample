namespace Application.Features.UserFeatures.UserRoles.Commands.Create;


public class CreatedUserRoleResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
