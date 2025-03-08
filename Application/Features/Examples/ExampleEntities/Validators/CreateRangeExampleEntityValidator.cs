using Application.Features.Examples.ExampleEntities.Commands;

namespace Application.Features.Examples.ExampleEntities.Validators;

public class CreateRangeExampleEntityValidator : AbstractValidator<CreateRangeExampleEntityCommand>
{
    public CreateRangeExampleEntityValidator()
    {
        // RuleFor(p => p.Names).NotEmpty().MinimumLength(2);
    }
}
