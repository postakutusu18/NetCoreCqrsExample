namespace Application.Features.Examples.ExampleEntities.Queries.AuthCheckAdd;

public record AuthCheckAddExampleEntityQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Add];
}
