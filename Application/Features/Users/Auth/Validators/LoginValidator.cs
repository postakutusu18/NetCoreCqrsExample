using Application.Features.Users.Auth.Commands;

namespace Application.Features.Users.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.UserForLoginDto.Password).NotEmpty().MinimumLength(4);
    }
}
