namespace Application.Features.UserFeatures.Roles.Queries.GetById;

public class GetByIdRoleResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetByIdRoleResponse()
    {
        Name = string.Empty;
    }

    public GetByIdRoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}