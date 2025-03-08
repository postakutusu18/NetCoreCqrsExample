using Application.Features.Users.UserRoles.Commands;

namespace Application.Features.Users.UserRoles.Validators;

public class CreateUserRoleValidator : AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleValidator()
    {
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.RoleId).NotNull();
    }
}