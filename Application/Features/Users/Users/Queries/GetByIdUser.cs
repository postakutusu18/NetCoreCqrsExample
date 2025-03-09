using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Rules;
using Core.Localization;

namespace Application.Features.Users.Queries.GetById;

    public class GetByIdUser : IRequestHandler<GetByIdUserQuery,IDataResult<GetByIdUserResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRules _userBusinessRules;
    private readonly ILocalizationService _localizationService;

    public GetByIdUser(IUnitOfWorkAsync unitOfWorkAsync, UserRules userBusinessRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userBusinessRules = userBusinessRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetByIdUserResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _unitOfWorkAsync.UserRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = user.Adapt<GetByIdUserResponse>();
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.SuccessRecord, UsersMessages.SectionName);
        var result = new SuccessDataResult<GetByIdUserResponse>(response, message);  
            return result;
        }
    }
public class GetByIdUserQuery : IRequest<IDataResult<GetByIdUserResponse>>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.ReadRole };
}
public record GetByIdUserResponse(Guid Id, string FirstName, string LastName, string Email, bool Status);
