using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Rules;
using Core.Localization;

namespace Application.Features.Users.Users.Commands;

public class DeleteUser : IRequestHandler<DeleteUserCommand, IDataResult<DeletedUserResponse>>
{
    //private readonly IUserDalAsync _userRepository;
    private readonly UserRules _userRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public DeleteUser(UserRules userRules, IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _userRules = userRules;
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
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
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.SuccessDeleted, UsersMessages.SectionName);
        var result = new SuccessDataResult<DeletedUserResponse>(response,message);
        return result;
    }
}

public record DeleteUserCommand(Guid Id) : IRequest<IDataResult<DeletedUserResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };
}
public record DeletedUserResponse(Guid Id) : IResponse;
