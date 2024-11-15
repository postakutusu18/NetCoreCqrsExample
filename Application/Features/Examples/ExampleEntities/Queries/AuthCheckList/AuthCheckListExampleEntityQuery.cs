namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckList;

public record AuthCheckListExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Read];
}

