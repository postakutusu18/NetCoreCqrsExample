﻿using Core.Persistance.Repositories;
using Domains.Users;

namespace Application.Repositories.Users;

public interface IRefreshTokenDalAsync : IAsyncRepository<RefreshToken, Guid>
{
    Task<List<RefreshToken>> GetOldRefreshTokensAsync(Guid userId, int refreshTokenTTL);
}