namespace Application.Features.Examples.ExampleEntities.Queries.GetById;

public class GetByIdExampleEntityQuery : IRequest<IDataResult<GetByIdExampleEntityResponse>>
{
    public Guid Id { get; set; }
}
