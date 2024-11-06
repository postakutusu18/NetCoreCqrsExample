using Application.Features.Users.Rules;
using Application.Repositories;
using Application.Repositories.Users;
using Core.Persistance.Paging;
using Domains.Users;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.UserService;

public class UserManager : IUserService
{
    //private readonly IUserDalAsync _userRepository;
    private readonly UserRules _userBusinessRules;
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    public UserManager(UserRules userBusinessRules, IUnitOfWorkAsync unitOfWorkAsync)
    {
        // _userRepository = userRepository;
        _userBusinessRules = userBusinessRules;
        _unitOfWorkAsync = unitOfWorkAsync;
    }

    public async Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        User? user = await _unitOfWorkAsync.UserRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<IPaginate<User>?> GetListAsync(
        Expression<Func<User, bool>>? predicate = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<User> userList = await _unitOfWorkAsync.UserRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<User> AddAsync(User user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);

        User addedUser = await _unitOfWorkAsync.UserRepository.AddAsync(user);

        return addedUser;
    }

    public async Task<User> UpdateAsync(User user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user.Id, user.Email);

        User updatedUser = await _unitOfWorkAsync.UserRepository.UpdateAsync(user);

        return updatedUser;
    }

    public async Task<User> DeleteAsync(User user, bool permanent = false)
    {
        User deletedUser = await _unitOfWorkAsync.UserRepository.DeleteAsync(user);

        return deletedUser;
    }
}