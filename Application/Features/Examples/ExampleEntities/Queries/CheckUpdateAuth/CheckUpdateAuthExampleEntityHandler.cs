namespace Application.Features.Examples.ExampleEntities.Queries.CheckUpdateAuth;

public class CheckUpdateAuthExampleEntityHandler : IRequestHandler<AuthCheckUpdateExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
