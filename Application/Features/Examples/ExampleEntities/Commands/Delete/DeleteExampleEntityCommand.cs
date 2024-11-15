namespace Application.Features.Examples.ExampleEntities.Commands.Delete;

public record DeleteExampleEntityCommand(Guid Id) : IRequest<IDataResult<DeletedExampleEntityResponse>>
{
}
