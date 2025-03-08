using Application.Features.UserFeatures.Users.Rules;
using Application.Services.AuthService;
using Core.Security.Hashing;
using Core.Security.Jwt;

namespace Application.Features.Users.Commands.UpdateFromAuth;



public class UpdateUserFromAuth : IRequestHandler<UpdateUserFromAuthCommand, IDataResult<UpdatedUserFromAuthResponse>>
{
    private readonly UserRules _userRules;
    private readonly IAuthService _authService;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    public UpdateUserFromAuth(
        UserRules userBusinessRules,
        IAuthService authService
,
        IUnitOfWorkAsync unitOfWorkAsync)
    {
        _userRules = userBusinessRules;
        _authService = authService;
        _unitOfWorkAsync = unitOfWorkAsync;
    }

    public async Task<IDataResult<UpdatedUserFromAuthResponse>> Handle(
        UpdateUserFromAuthCommand request,
        CancellationToken cancellationToken
    )
    {
        User? user = await _unitOfWorkAsync.UserRepository.GetAsync(
            predicate: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken
        );
        await _userRules.UserShouldBeExistsWhenSelected(user);
        await _userRules.UserPasswordShouldBeMatched(user: user!, request.Password);
        await _userRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);

        user = request.Adapt<User>();
        if (request.NewPassword != null && !string.IsNullOrWhiteSpace(request.NewPassword))
        {
            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user!.PasswordHash = passwordHash;
            user!.PasswordSalt = passwordSalt;
        }

        User updatedUser = await _unitOfWorkAsync.UserRepository.UpdateAsync(user!);
        await _unitOfWorkAsync.SaveAsync();

        UpdatedUserFromAuthResponse response = updatedUser.Adapt<UpdatedUserFromAuthResponse>();
        response.AccessToken = await _authService.CreateAccessToken(user!);
        var result = new SuccessDataResult<UpdatedUserFromAuthResponse>(response);
        return result;
    }
}

public partial class UpdateUserFromAuthCommand : IRequest<IDataResult<UpdatedUserFromAuthResponse>>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

   
}
public class UpdatedUserFromAuthResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AccessToken AccessToken { get; set; }

}
