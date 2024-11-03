using Application.Features.Users.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,IDataResult<DeletedUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserRules _userRules;

    public DeleteUserCommandHandler(IUserRepository userRepository, UserRules userRules)
    {
        _userRepository = userRepository;
        _userRules = userRules;
    }

    public async Task<IDataResult<DeletedUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetAsync(
            predicate: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken
        );
        await _userRules.UserShouldBeExistsWhenSelected(user);

        await _userRepository.DeleteAsync(user!);

        DeletedUserResponse response = user.Adapt<DeletedUserResponse>();
        var result = new SuccessDataResult<DeletedUserResponse>(response);
        return result;
    }
}