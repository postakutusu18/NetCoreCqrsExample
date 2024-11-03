using Application.Features.Users.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Core.Security.Hashing;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,IDataResult<CreatedUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserRules _userRules;

    public CreateUserCommandHandler(IUserRepository userRepository, UserRules userRules)
    {
        _userRepository = userRepository;
        _userRules = userRules;
    }

    public async Task<IDataResult<CreatedUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _userRules.UserEmailShouldNotExistsWhenInsert(request.Email);
        User user = request.Adapt<User>();

        HashingHelper.CreatePasswordHash(
            request.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User createdUser = await _userRepository.AddAsync(user);

        CreatedUserResponse response = createdUser.Adapt<CreatedUserResponse>();
        var result = new SuccessDataResult<CreatedUserResponse>(response);
        return result;
    }
}