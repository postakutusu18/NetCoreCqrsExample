using Application.Features.UserFeatures.Auth.Rules;
using Application.Services.AuthService;
using Core.Application.Dtos;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.Jwt;

namespace Application.Features.Users.Auth.Commands;

public class RegisterProcess : IRequestHandler<RegisterCommand, RegisteredResponse>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;

    public RegisterProcess(
        IUnitOfWorkAsync unitOfWorkAsync,
        IAuthService authService,
        AuthBusinessRules authBusinessRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _authService = authService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

        HashingHelper.CreatePasswordHash(
            request.UserForRegisterDto.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        User newUser =
            new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                AuthenticatorType = AuthenticatorType.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        User createdUser = await _unitOfWorkAsync.UserRepository.AddAsync(newUser);
        await _unitOfWorkAsync.SaveAsync();
        AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

        RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
            createdUser,
            request.IpAddress
        );
        RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

        RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
        return registeredResponse;
    }
}
public partial class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }
}
public class RegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
