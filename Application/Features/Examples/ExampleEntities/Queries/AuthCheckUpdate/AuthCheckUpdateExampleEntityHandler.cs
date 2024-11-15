namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckUpdate;

public class AuthCheckUpdateExampleEntityHandler : IRequestHandler<AuthCheckUpdateExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
