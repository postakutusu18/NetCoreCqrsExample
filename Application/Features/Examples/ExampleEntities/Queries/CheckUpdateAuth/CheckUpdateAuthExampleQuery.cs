namespace Application.Features.Examples.ExampleEntities.Queries.CheckUpdateAuth;

public record AuthCheckUpdateExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Update];
}
