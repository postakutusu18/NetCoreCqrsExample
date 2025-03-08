namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckAddAuthExampleEntity : IRequestHandler<CheckAddAuthExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(CheckAddAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record CheckAddAuthExampleEntityQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Add];
}
