namespace Application.Features.UserFeatures.Auth.Constants;

public static class AuthOperationClaims
{
    private const string _section = "Auth";

    public const string AdminRole = $"{_section}.Admin";

    public const string WriteRole = $"{_section}.Write";
    public const string ReadRole = $"{_section}.Read";

    public const string RevokeTokenRole = $"{_section}.RevokeToken";
}
