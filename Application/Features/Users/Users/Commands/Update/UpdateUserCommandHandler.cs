﻿using Application.Features.UserFeatures.Users.Rules;
using Core.Security.Hashing;
using Domains.Users;

namespace Application.Features.UserFeatures.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IDataResult<UpdatedUserResponse>>
{
    //private readonly IUserDalAsync _userRepository;
    private readonly UserRules _userRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;

    public UpdateUserCommandHandler(UserRules userRules, IUnitOfWorkAsync unitOfWorkAsync)
    {
        //   _userRepository = userRepository;
        _userRules = userRules;
        _unitOfWorkAsync = unitOfWorkAsync;
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
        var result = new SuccessDataResult<UpdatedUserResponse>(response);
        return result;
    }
}