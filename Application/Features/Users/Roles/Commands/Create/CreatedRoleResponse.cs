using Core.Application.Responses;

namespace Application.Features.UserFeatures.Roles.Commands.Create;

public class CreatedRoleResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CreatedRoleResponse()
    {
        Name = string.Empty;
    }

    public CreatedRoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
