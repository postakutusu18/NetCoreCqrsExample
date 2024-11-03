namespace Application.Features.Roles.Constants;

public static class RoleOperationClaims
{
    private const string _section = "Roles";

    public const string AdminRole = $"{_section}.Admin";

    public const string ReadRole = $"{_section}.Read";
    public const string WriteRole = $"{_section}.Write";

    public const string CreateRole = $"{_section}.Create";
    public const string UpdateRole = $"{_section}.Update";
    public const string DeleteRole = $"{_section}.Delete";
}
