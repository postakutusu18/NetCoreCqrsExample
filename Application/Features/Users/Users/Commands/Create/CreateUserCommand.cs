using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.UserFeatures.Users.Commands.Create;

public class CreateUserCommand : IRequest<IDataResult<CreatedUserResponse>>, ISecuredRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateUserCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public CreateUserCommand(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.CreateRole };


}
