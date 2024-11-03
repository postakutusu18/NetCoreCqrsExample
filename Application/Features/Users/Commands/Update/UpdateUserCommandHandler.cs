using Application.Features.Users.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Core.Security.Hashing;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,IDataResult<UpdatedUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserRules _userRules;

    public UpdateUserCommandHandler(IUserRepository userRepository, UserRules userRules)
    {
        _userRepository = userRepository;
        _userRules = userRules;
    }

    public async Task<IDataResult<UpdatedUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetAsync(
            predicate: u => u.Id.Equals(request.Id),
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _userRules.UserShouldBeExistsWhenSelected(user);
        await _userRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);
        user = request.Adapt<User>();

        HashingHelper.CreatePasswordHash(
            request.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        user!.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _userRepository.UpdateAsync(user);

        UpdatedUserResponse response = user.Adapt<UpdatedUserResponse>();
        var result = new SuccessDataResult<UpdatedUserResponse>(response);
        return result;
    }
}