using Application.Features.Examples.Products.Commands;

namespace Application.Features.Examples.Products.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}
