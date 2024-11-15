using Application.Features.UserFeatures.Users.Constants;
using Application.Repositories;
using Application.Repositories.Users;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization;
using Core.Security.Hashing;
using Domains.Users;

namespace Application.Features.UserFeatures.Users.Rules;


public class UserRules : BaseBusinessRules
{
    //private readonly IUserDalAsync _userRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;

    public UserRules(ILocalizationService localizationService, IUnitOfWorkAsync unitOfWorkAsync)
    {
        _localizationService = localizationService;
        _unitOfWorkAsync = unitOfWorkAsync;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UsersMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(UsersMessages.UserDontExists);
    }

    public async Task UserIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await _unitOfWorkAsync.UserRepository.AnyAsync(predicate: u => u.Id == id);
        if (doesExist)
            await throwBusinessException(UsersMessages.UserDontExists);
    }

    public async Task UserPasswordShouldBeMatched(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            await throwBusinessException(UsersMessages.PasswordDontMatch);
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _unitOfWorkAsync.UserRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(UsersMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(Guid id, string email)
    {
        bool doesExists = await _unitOfWorkAsync.UserRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
        if (doesExists)
            await throwBusinessException(UsersMessages.UserMailAlreadyExists);
    }
}
