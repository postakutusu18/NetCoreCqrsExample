namespace Application.Features.UserFeatures.Roles.Queries.GetList;

public class GetListRoleResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetListRoleResponse()
    {
        Name = string.Empty;
    }

    public GetListRoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
