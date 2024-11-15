using Core.Application.Responses;

namespace Application.Features.UserFeatures.Users.Commands.Delete;

public class DeletedUserResponse : IResponse
{
    public Guid Id { get; set; }
}
