﻿using Core.Persistance.DbHelper;
using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Core.Persistance.PagingAjax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Persistance.Repositories;



public class EfAsyncRepositoryBase<TEntity, TEntityId> : IAsyncRepository<TEntity, TEntityId>, IQuery<TEntity> where TEntity : Entity<TEntityId>
{
    protected readonly DbContext _dbContext;

    public EfAsyncRepositoryBase(DbContext context)
    {
        _dbContext = context;
    }

    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>();
    }

    protected virtual void EditEntityPropertiesToAdd(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        EditEntityPropertiesToAdd(entity);
        await _dbContext.AddAsync(entity, cancellationToken);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (TEntity entity in entities)
        {
            EditEntityPropertiesToAdd(entity);
        }

        await _dbContext.AddRangeAsync(entities, cancellationToken);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entities;
    }

    protected virtual void EditEntityPropertiesToUpdate(TEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        EditEntityPropertiesToUpdate(entity);
        _dbContext.Update(entity);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (TEntity entity in entities)
        {
            EditEntityPropertiesToUpdate(entity);
        }

        _dbContext.UpdateRange(entities);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        await SetEntityAsDeleted(entity, permanent, isAsync: true, cancellationToken);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        await SetEntityAsDeleted(entities, permanent, isAsync: true, cancellationToken);
        //await _dbContext.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (withDeleted)
        {
            queryable = queryable.IgnoreQueryFilters();
        }

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        }

        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (withDeleted)
        {
            queryable = queryable.IgnoreQueryFilters();
        }

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (withDeleted)
        {
            queryable = queryable.IgnoreQueryFilters();
        }

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> source = Query();
        if (withDeleted)
        {
            source = source.IgnoreQueryFilters();
        }

        if (predicate != null)
        {
            source = source.Where(predicate);
        }

        return await source.AnyAsync(cancellationToken);
    }


    protected async Task SetEntityAsDeleted(TEntity entity, bool permanent, bool isAsync = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (!permanent)
        {
            CheckHasEntityHaveOneToOneRelation(entity);
            if (isAsync)
            {
                await setEntityAsSoftDeleted(entity, isAsync, cancellationToken);
            }
            else
            {
                setEntityAsSoftDeleted(entity, isAsync).Wait();
            }
        }
        else
        {
            _dbContext.Remove(entity);
        }
    }

    protected async Task SetEntityAsDeleted(IEnumerable<TEntity> entities, bool permanent, bool isAsync = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (TEntity entity in entities)
        {
            await SetEntityAsDeleted(entity, permanent, isAsync, cancellationToken);
        }
    }
    protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
    {
        TEntity entity2 = entity;
        IForeignKey foreignKey = _dbContext.Entry(entity2).Metadata.GetForeignKeys().FirstOrDefault((IForeignKey fk) => fk.IsUnique && fk.PrincipalKey.Properties.All((IProperty pk) => _dbContext.Entry(entity2).Property(pk.Name).Metadata.IsPrimaryKey()));
        if (foreignKey != null)
        {
            string name = foreignKey.PrincipalEntityType.ClrType.Name;
            IReadOnlyList<IProperty> properties = _dbContext.Entry(entity2).Metadata.FindPrimaryKey().Properties;
            string value = string.Join(", ", properties.Select((IProperty prop) => prop.Name));
            throw new InvalidOperationException($"Entity {entity2.GetType().Name} has a one-to-one relationship with {name} via the primary key ({value}). Soft Delete causes problems if you try to create an entry again with the same foreign key.");
        }
    }
    private async Task setEntityAsSoftDeleted(IEntityTimestamps entity, bool isAsync = true, CancellationToken cancellationToken = default(CancellationToken), bool isRoot = true)
    {
        if (IsSoftDeleted(entity))
        {
            return;
        }

        if (isRoot)
        {
            EditEntityPropertiesToDelete((TEntity)entity);
        }
        else
        {
            EditRelationEntityPropertiesToCascadeSoftDelete(entity);
        }

        List<INavigation> list = _dbContext.Entry(entity).Metadata.GetNavigations().Where(delegate (INavigation x)
        {
            if (x != null && !x.IsOnDependent)
            {
                IForeignKey foreignKey = x.ForeignKey;
                if (foreignKey != null)
                {
                    DeleteBehavior deleteBehavior = foreignKey.DeleteBehavior;
                    if ((uint)(deleteBehavior - 3) <= 1u)
                    {
                        return true;
                    }
                }
            }

            return false;
        }).ToList();
        foreach (INavigation item in list)
        {
            if (item.TargetEntityType.IsOwned() || item.PropertyInfo == null)
            {
                continue;
            }

            object obj = item.PropertyInfo.GetValue(entity);
            if (item.IsCollection)
            {
                if (obj == null)
                {
                    IQueryable query = _dbContext.Entry(entity).Collection(item.PropertyInfo.Name).Query();
                    if (isAsync)
                    {
                        IQueryable<object> relationLoaderQuery = GetRelationLoaderQuery(query, item.PropertyInfo.GetType());
                        if (relationLoaderQuery != null)
                        {
                            obj = await relationLoaderQuery.ToListAsync(cancellationToken);
                        }
                    }
                    else
                    {
                        obj = GetRelationLoaderQuery(query, item.PropertyInfo.GetType())?.ToList();
                    }

                    if (obj == null)
                    {
                        continue;
                    }
                }

                foreach (object item2 in (IEnumerable)obj)
                {
                    await setEntityAsSoftDeleted((IEntityTimestamps)item2, isAsync, cancellationToken, isRoot: false);
                }

                continue;
            }

            if (obj == null)
            {
                IQueryable query2 = _dbContext.Entry(entity).Reference(item.PropertyInfo.Name).Query();
                if (isAsync)
                {
                    IQueryable<object> relationLoaderQuery2 = GetRelationLoaderQuery(query2, item.PropertyInfo.GetType());
                    if (relationLoaderQuery2 != null)
                    {
                        obj = await relationLoaderQuery2.FirstOrDefaultAsync(cancellationToken);
                    }
                }
                else
                {
                    obj = GetRelationLoaderQuery(query2, item.PropertyInfo.GetType())?.FirstOrDefault();
                }

                if (obj == null)
                {
                    continue;
                }
            }

            await setEntityAsSoftDeleted((IEntityTimestamps)obj, isAsync, cancellationToken, isRoot: false);
        }

        _dbContext.Update(entity);
    }

    protected IQueryable<object>? GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
    {
        return ((IQueryable<object>)(query.Provider.GetType().GetMethods().First((MethodInfo m) => (object)m != null && m.Name == "CreateQuery" && m.IsGenericMethod)?.MakeGenericMethod(navigationPropertyType) ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.")).Invoke(query.Provider, new object[1] { query.Expression })).Where((object x) => !((IEntityTimestamps)x).DeletedDate.HasValue);
    }
    protected virtual void EditEntityPropertiesToDelete(TEntity entity)
    {
        entity.DeletedDate = DateTime.UtcNow;
        entity.IsDelete = true;
    }

    protected virtual void EditRelationEntityPropertiesToCascadeSoftDelete(IEntityTimestamps entity)
    {
        entity.DeletedDate = DateTime.UtcNow;
        entity.IsDelete = true;
    }

    protected virtual bool IsSoftDeleted(IEntityTimestamps entity)
    {
        return entity.DeletedDate.HasValue;
    }
    public async Task<PagingResult<TEntity>> GetListAjaxAsync(DataTableAjaxDto globalFilter, Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProporties)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();       
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (includeProporties != null)
        {
            if (includeProporties.Any())
            {
                foreach (var item in includeProporties)
                {
                    query = query.Include(item);
                }
            }
        }
        if (globalFilter == null)
        {
            var dataFilter = predicate != null ? await query.Where(predicate).ToListAsync() : await query.ToListAsync();

            var count = predicate != null ? query.Where(predicate).Count() : query.Count();
            return new PagingResult<TEntity>(dataFilter, count);
        }

        var parameterOfExpression = Expression.Parameter(typeof(TEntity), "x");

        var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[] { });


        if (globalFilter.PropertyField is not null &&globalFilter.PropertyField.Count > 0 && !string.IsNullOrEmpty(globalFilter.SearchText))
        {
            var containMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            var searchedValue = Expression.Constant(globalFilter.SearchText.ToLower(), typeof(string));

            var globalFilterPropertyField = Expression.PropertyOrField(parameterOfExpression, globalFilter.PropertyField[0]);

            Expression finalExpression = Expression.Call(Expression.Call(globalFilterPropertyField, toLowerMethod), containMethod, searchedValue);

            for (int i = 1; i < globalFilter.PropertyField.Count; i++)
            {
                var propertyName = globalFilter.PropertyField[i];
                if (!propertyName.Contains("."))
                {
                    globalFilterPropertyField = Expression.PropertyOrField(parameterOfExpression, propertyName);
                    var globalFilterConstant = Expression.Call(Expression.Call(globalFilterPropertyField, toLowerMethod), containMethod, searchedValue);

                    finalExpression = Expression.Or(finalExpression, globalFilterConstant);
                }

            }

            var list = predicate != null ? query.Where(predicate)
                .Where(Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameterOfExpression)) :
                query.Where(Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameterOfExpression));
            var totalCountForFilter = list.Count();

            list = list.AscOrDescOrder(globalFilter.SortOrder == 1 ? SortEnums.ASC : SortEnums.DESC,
                globalFilter.SortField).Skip(globalFilter.First).Take(globalFilter.Rows);


            return new PagingResult<TEntity>(list.ToList(), totalCountForFilter);
        }

        //Is no have search text
        var totalCount = predicate != null ?
            await query.Where(predicate).CountAsync() :
            await query.CountAsync();
        var data = predicate != null ?
            await query.Where(predicate).Skip(globalFilter.First).Take(globalFilter.Rows).ToListAsync() :
            await query.Skip(globalFilter.First).Take(globalFilter.Rows).ToListAsync();
        return new PagingResult<TEntity>(data, totalCount);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await _dbContext.Set<TEntity>().CountAsync(predicate);
    }

    public async Task<int> DeleteSqlRawAsync(string query)
    {
        return await _dbContext.Database.ExecuteSqlRawAsync(query);
    }
}
