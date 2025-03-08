namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckListAuthExampleEntity : IRequestHandler<CheckListAuthExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(CheckListAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record CheckListAuthExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Read];
}

