using Application.Features.Users.Rules;
using Application.Repositories.Users;
using Application.Services.AuthService;
using Core.Application.Results;
using Core.Security.Hashing;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.UpdateFromAuth;


public partial class UpdateUserFromAuthCommand
{
    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, IDataResult<UpdatedUserFromAuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserRules _userRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(
            IUserRepository userRepository,
            UserRules userBusinessRules,
            IAuthService authService
        )
        {
            _userRepository = userRepository;
            _userRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<IDataResult<UpdatedUserFromAuthResponse>> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userRepository.GetAsync(
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

            User updatedUser = await _userRepository.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = updatedUser.Adapt<UpdatedUserFromAuthResponse>();
            response.AccessToken = await _authService.CreateAccessToken(user!);
            var result = new SuccessDataResult<UpdatedUserFromAuthResponse>(response);
            return result;
        }
    }
}
