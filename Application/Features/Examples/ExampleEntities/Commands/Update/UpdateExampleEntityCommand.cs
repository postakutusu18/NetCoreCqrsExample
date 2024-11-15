namespace Application.Features.Examples.ExampleEntities.Commands.Update;

public record UpdateExampleEntityCommand(Guid Id, string Name) : IRequest<IDataResult<UpdatedExampleEntityResponse>>
{
}
