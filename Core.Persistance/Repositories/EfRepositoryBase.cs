﻿using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Persistance.Repositories;


public class EfRepositoryBase<TEntity, TEntityId> : IRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId>
{
    protected readonly DbContext _dbContext;

    public EfRepositoryBase(DbContext context)
    {

    }
    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>();
    }

    protected virtual void EditEntityPropertiesToAdd(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
    }
    public TEntity Add(TEntity entity)
    {
        EditEntityPropertiesToAdd(entity);
        _dbContext.Add(entity);
        //_dbContext.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            EditEntityPropertiesToAdd(entity);
        }

        _dbContext.AddRange(entities);
        //_dbContext.SaveChanges();
        return entities;
    }

    public TEntity Update(TEntity entity)
    {
        EditEntityPropertiesToAdd(entity);
        _dbContext.Update(entity);
        //_dbContext.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            EditEntityPropertiesToAdd(entity);
        }

        _dbContext.UpdateRange(entities);
        //_dbContext.SaveChanges();
        return entities;
    }

    public TEntity Delete(TEntity entity, bool permanent = false)
    {
        SetEntityAsDeleted(entity, permanent, isAsync: false).Wait();
        //_dbContext.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false)
    {
        SetEntityAsDeleted(entities, permanent, isAsync: false).Wait();
        //_dbContext.SaveChanges();
        return entities;
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true)
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

        return queryable.FirstOrDefault(predicate);
    }

    public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true)
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
            return orderBy(queryable).ToPaginate(index, size);
        }

        return queryable.ToPaginate(index, size);
    }

    public IPaginate<TEntity> GetListByDynamic(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true)
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

        return queryable.ToPaginate(index, size);
    }

    public bool Any(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false)
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

        return source.Any();
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

    protected IQueryable<object>? GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
    {
        return ((IQueryable<object>)(query.Provider.GetType().GetMethods().First((MethodInfo m) => (object)m != null && m.Name == "CreateQuery" && m.IsGenericMethod)?.MakeGenericMethod(navigationPropertyType) ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.")).Invoke(query.Provider, new object[1] { query.Expression })).Where((object x) => !((IEntityTimestamps)x).DeletedDate.HasValue);
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
}
