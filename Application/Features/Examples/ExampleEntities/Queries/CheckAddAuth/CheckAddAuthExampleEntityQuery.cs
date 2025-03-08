namespace Application.Features.Examples.ExampleEntities.Queries.CheckAddAuth;

public record CheckAddAuthExampleEntityQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Add];
}
