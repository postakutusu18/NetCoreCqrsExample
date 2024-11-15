namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckAdd;

public class AuthCheckAddExampleEntityHandler : IRequestHandler<AuthCheckAddExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}