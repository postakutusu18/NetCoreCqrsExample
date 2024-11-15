using Application.Features.UserFeatures.Users.Commands.UpdateFromAuth;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public partial class UpdateUserFromAuthCommand : IRequest<IDataResult<UpdatedUserFromAuthResponse>>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateUserFromAuthCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserFromAuthCommand(Guid id, string firstName, string lastName, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
