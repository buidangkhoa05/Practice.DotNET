using Domain.Common.PagedList;
using Domain.Common.Queries.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Repository
{
    internal partial class RepositoryBase<TEntity>
    {
        public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, bool>>? expression = null,
            CancellationToken cancellationToken = default
        ) where TResult : class
        {
            expression ??= _ => true;

            return await QueryAsNoTracking
                .Where(expression)
                .ProjectToType<TResult>()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
           Expression<Func<TEntity, bool>>? expression = null,
           CancellationToken cancellationToken = default)
        {
            expression ??= _ => true;

            return await QueryAsNoTracking
                .Where(expression)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> AreAllExistedAsync(IEnumerable<object> keys)
        {
            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext));

            if (keys == null || !keys.Any())
                return true;

            var keyProperty = GetPrimaryKeyProperty();

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var propertyAccess = Expression.Property(parameter, keyProperty.Name);
            var keyValues = keys.Cast<object>().ToList();

            var body = keyValues
                .Select(key => Expression.Equal(propertyAccess, Expression.Constant(key)))
                .Aggregate<Expression>((accumulate, equal) => Expression.OrElse(accumulate, equal));

            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            var count = await QueryAsNoTracking
                .CountAsync(lambda)
                .ConfigureAwait(false);

            return count == keyValues.Count;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return await QueryAsNoTracking
                .AnyAsync(expression ?? (_ => true))
                .ConfigureAwait(false);
        }

        #region Search function
        public virtual async Task<IPagedList<TResult>> SearchAsync<TSearchRequest, TResult>(
           TSearchRequest request)
           where TSearchRequest : SearchBaseRequest
           where TResult : class
        {
            var query = _dbSet.AsNoTracking();

            query = ApplyIncludeOperator4Search(query);

            query = query.Where(GetSearchFilterExpression(request));

            query = query.WithOrderByString(request.OrderBy);

            return await query.ToPagedListAsync<TEntity, TResult>(request.PagingQuery);
        }

        protected virtual Expression<Func<TEntity, bool>> GenerateSearchExpression<TSearchRequest>(
            TSearchRequest request)
            where TSearchRequest : SearchBaseRequest
        {
            return x => true;
        }

        protected virtual IQueryable<TEntity> ApplyIncludeOperator4Search(IQueryable<TEntity> query)
        {
            return query;
        }

        protected virtual 
        #endregion Search function
    }
}
