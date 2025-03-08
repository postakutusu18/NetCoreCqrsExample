namespace Application.Features.Examples.ExampleEntities.Queries.CheckListAuth;

public class CheckListAuthExampleEntityHandler : IRequestHandler<CheckListAuthExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(CheckListAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

