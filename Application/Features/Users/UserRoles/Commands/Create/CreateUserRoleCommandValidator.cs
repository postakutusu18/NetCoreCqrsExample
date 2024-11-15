using FluentValidation;

namespace Application.Features.UserFeatures.UserRoles.Commands.Create;

public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.RoleId).NotNull();
    }
}