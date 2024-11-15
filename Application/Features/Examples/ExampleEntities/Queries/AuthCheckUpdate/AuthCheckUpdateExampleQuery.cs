namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckUpdate;

public record AuthCheckUpdateExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Update];
}
