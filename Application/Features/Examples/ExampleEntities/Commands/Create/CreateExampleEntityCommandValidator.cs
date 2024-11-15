
namespace Application.Features.Examples.ExampleEntities.Commands.Create;

public class CreateExampleEntityCommandValidator : AbstractValidator<CreateExampleEntityCommand>
{
    public CreateExampleEntityCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}
