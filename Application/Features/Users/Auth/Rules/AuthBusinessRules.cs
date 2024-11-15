using Application.Features.UserFeatures.Auth.Constants;
using Core.Security.Enums;
using Core.Security.Hashing;

namespace Application.Features.UserFeatures.Auth.Rules;


public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public AuthBusinessRules(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthMessages.SectionName);
        throw new BusinessException(message);
    }

    //public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    //{
    //    if (emailAuthenticator is null)
    //        await throwBusinessException(AuthMessages.EmailAuthenticatorDontExists);
    //}

    //public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is null)
    //        await throwBusinessException(AuthMessages.OtpAuthenticatorDontExists);
    //}

    //public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
    //        await throwBusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    //}

    //public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    //{
    //    if (emailAuthenticator.ActivationKey is null)
    //        await throwBusinessException(AuthMessages.EmailActivationKeyDontExists);
    //}

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            await throwBusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    }

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            await throwBusinessException(AuthMessages.RefreshDontExists);
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate != null && DateTime.UtcNow >= refreshToken.ExpirationDate)
            await throwBusinessException(AuthMessages.InvalidRefreshToken);
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _unitOfWorkAsync.UserRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            await throwBusinessException(AuthMessages.PasswordDontMatch);
    }
}
