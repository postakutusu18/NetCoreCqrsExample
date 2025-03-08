using Application.Features.Examples.ExampleEntities.Commands;

namespace Application.Features.Examples.ExampleEntities.Validators;

public class CreateExampleEntityValidator : AbstractValidator<CreateExampleEntityCommand>
{
    public CreateExampleEntityValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}
