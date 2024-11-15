namespace Application.Features.Examples.ExampleEntities.Commands.Create;

public record CreatedExampleEntityResponse(Guid Id, string Name) : IResponse { }
