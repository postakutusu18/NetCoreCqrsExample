namespace Application.Features.Examples.ExampleEntities.Queries.CheckAddAuth;

public class CheckAddAuthExampleEntityHandler : IRequestHandler<CheckAddAuthExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(CheckAddAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}