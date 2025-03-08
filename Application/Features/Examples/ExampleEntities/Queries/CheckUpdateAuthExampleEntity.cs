namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckUpdateAuthExampleEntity : IRequestHandler<AuthCheckUpdateExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
public record AuthCheckUpdateExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Update];
}
