namespace Application.Features.Examples.ExampleEntities.Queries.CheckListAuth;

public record CheckListAuthExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Read];
}

