using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductQuery : IRequest<IDataResult<GetByIdProductResponse>>
{
    public Guid Id { get; set; }
}
