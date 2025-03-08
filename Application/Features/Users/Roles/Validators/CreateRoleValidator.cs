using Application.Features.Users.Roles.Commands;

namespace Application.Features.Users.Roles.Validators;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}
