using Application.Features.UserFeatures.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Dtos;
using Core.Security.Enums;
using Core.Security.Jwt;

namespace Application.Features.Users.Auth.Commands;

public class LoginProcess : IRequestHandler<LoginCommand, IDataResult<LoggedResponse>>
{
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public LoginProcess(
        IUserService userService,
        IAuthService authService,
        AuthBusinessRules authBusinessRules
    )
    {
        _userService = userService;
        _authService = authService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<IDataResult<LoggedResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userService.GetAsync(
            predicate: u => u.Email == request.UserForLoginDto.Email,
            cancellationToken: cancellationToken
        );
        await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
        await _authBusinessRules.UserPasswordShouldBeMatch(user!, request.UserForLoginDto.Password);

        LoggedResponse loggedResponse = new();

        AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

        RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
        RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
        await _authService.DeleteOldRefreshTokens(user.Id);

        loggedResponse.AccessToken = createdAccessToken;
        loggedResponse.RefreshToken = addedRefreshToken;

        return new SuccessDataResult<LoggedResponse>(loggedResponse, "success login");
    }
}

public class LoggedResponse : IResponse
{
    public AccessToken? AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }
    public AuthenticatorType? RequiredAuthenticatorType { get; set; }

    public LoggedHttpResponse ToHttpResponse()
    {
        return new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType };
    }

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}

public partial class LoginCommand : IRequest<IDataResult<LoggedResponse>>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }
}
