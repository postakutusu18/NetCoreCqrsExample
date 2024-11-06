using Application.Features.Users.Rules;
using Application.Repositories;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,IDataResult<DeletedUserResponse>>
{
    //private readonly IUserDalAsync _userRepository;
    private readonly UserRules _userRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;

    public DeleteUserCommandHandler(UserRules userRules, IUnitOfWorkAsync unitOfWorkAsync)
    {
        _userRules = userRules;
        _unitOfWorkAsync = unitOfWorkAsync;
    }

    public async Task<IDataResult<DeletedUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWorkAsync.UserRepository.GetAsync(
            predicate: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken
        );
        await _userRules.UserShouldBeExistsWhenSelected(user);

        await _unitOfWorkAsync.UserRepository.DeleteAsync(user!);
        await _unitOfWorkAsync.SaveAsync();

        DeletedUserResponse response = user.Adapt<DeletedUserResponse>();
        var result = new SuccessDataResult<DeletedUserResponse>(response);
        return result;
    }
}