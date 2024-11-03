using Application.Features.Users.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public partial class GetByIdUserQuery
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery,IDataResult<GetByIdUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, UserRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<IDataResult<GetByIdUserResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = user.Adapt<GetByIdUserResponse>();
            var result = new SuccessDataResult<GetByIdUserResponse>(response);  
            return result;
        }
    }
}
