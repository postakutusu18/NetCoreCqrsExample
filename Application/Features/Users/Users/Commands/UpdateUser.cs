using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Rules;
using Core.Localization;
using Core.Security.Hashing;

namespace Application.Features.Users.Users.Commands;

public class UpdateUser : IRequestHandler<UpdateUserCommand, IDataResult<UpdatedUserResponse>>
{
    //private readonly IUserDalAsync _userRepository;
    private readonly UserRules _userRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public UpdateUser(UserRules userRules, IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        //   _userRepository = userRepository;
        _userRules = userRules;
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<UpdatedUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWorkAsync.UserRepository.GetAsync(
            predicate: u => u.Id.Equals(request.Id),
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _userRules.UserShouldBeExistsWhenSelected(user);
        await _userRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);
        var mappedUserRole = request.Adapt(user);
        HashingHelper.CreatePasswordHash(
            request.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        user!.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _unitOfWorkAsync.UserRepository.UpdateAsync(mappedUserRole);
        await _unitOfWorkAsync.SaveAsync();

        UpdatedUserResponse response = user.Adapt<UpdatedUserResponse>();
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.SuccessUpdated, UsersMessages.SectionName);
        var result = new SuccessDataResult<UpdatedUserResponse>(response,message);
        return result;
    }
}

public record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Email, string Password) : IRequest<IDataResult<UpdatedUserResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };
}

public record UpdatedUserResponse(Guid Id, string FirstName, string LastName, string Email, bool Status);
