namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckList;

public class AuthCheckListExampleEntityHandler : IRequestHandler<AuthCheckListExampleEntityQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

