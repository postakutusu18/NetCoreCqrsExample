using Core.Application.Responses;

namespace Application.Features.Roles.Commands.Update;

public class UpdatedRoleResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdatedRoleResponse()
    {
        Name = string.Empty;
    }

    public UpdatedRoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
