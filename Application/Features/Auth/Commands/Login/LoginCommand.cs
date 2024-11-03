using Core.Application.Dtos;
using Core.Application.Responses;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Auth.Commands.Login;


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
