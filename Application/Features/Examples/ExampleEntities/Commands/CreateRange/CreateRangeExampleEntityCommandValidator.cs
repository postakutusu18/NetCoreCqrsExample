namespace Application.Features.Examples.ExampleEntities.Commands.CreateRange;

public class CreateRangeExampleEntityCommandValidator : AbstractValidator<CreateRangeExampleEntityCommand>
{
    public CreateRangeExampleEntityCommandValidator()
    {
       // RuleFor(p => p.Names).NotEmpty().MinimumLength(2);
    }
}
