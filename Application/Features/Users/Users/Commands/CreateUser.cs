using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Rules;
using Core.Security.Hashing;

namespace Application.Features.Users.Users.Commands;

public class CreateUser : IRequestHandler<CreateUserCommand, IDataResult<CreatedUserResponse>>
{
    private readonly UserRules _userRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;

    public CreateUser(UserRules userRules, IUnitOfWorkAsync unitOfWorkAsync)
    {
        _userRules = userRules;
        _unitOfWorkAsync = unitOfWorkAsync;
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
        User createdUser = await _unitOfWorkAsync.UserRepository.AddAsync(user);
        await _unitOfWorkAsync.SaveAsync();

        CreatedUserResponse response = createdUser.Adapt<CreatedUserResponse>();
        var result = new SuccessDataResult<CreatedUserResponse>(response);
        return result;
    }
}

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<IDataResult<CreatedUserResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.CreateRole };
}
public record CreatedUserResponse(Guid Id, string FirstName, string LastName, string Email, bool Status) : IResponse;
