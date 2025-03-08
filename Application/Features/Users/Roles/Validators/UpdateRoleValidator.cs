using Application.Features.Users.Roles.Commands;

namespace Application.Features.Users.Roles.Validators;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}
