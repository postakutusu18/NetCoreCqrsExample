﻿using Application.Features.UserFeatures.Users.Queries.GetById;
using Application.Features.UserFeatures.Users.Rules;

namespace Application.Features.Users.Queries.GetById;

public partial class GetByIdUserQuery
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery,IDataResult<GetByIdUserResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUnitOfWorkAsync unitOfWorkAsync, UserRules userBusinessRules)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _userBusinessRules = userBusinessRules;
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
            var result = new SuccessDataResult<GetByIdUserResponse>(response);  
            return result;
        }
    }
}
