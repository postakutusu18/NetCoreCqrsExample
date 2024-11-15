using Application.Features.UserFeatures.Users.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserFeatures.Users.Commands.Update;

public class UpdateUserCommand : IRequest<IDataResult<UpdatedUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UpdateUserCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserCommand(Guid id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };


}