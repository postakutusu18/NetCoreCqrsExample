namespace Application.Features.Example.Products.Queries.GetById;

public record GetByIdProductQuery(Guid Id) : IRequest<IDataResult<GetByIdProductResponse>>;
