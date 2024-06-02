
using System.Data.Entity;
using LinqKit;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Domain.Common.PagedList;
using Mapster;
using Domain.Common.Queries.Interface;

namespace Persistence.Common
{
    internal static class EntityFrameworlExtensions
    {
        public static IQueryable<TEntity> WhereWithExist<TEntity>(this IQueryable<TEntity> query,
           Expression<Func<TEntity, bool>>? condition = null)
           where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            if (condition != null)
            {
                return query.Where(condition.AddExistCondition());
            }

            return query;
        }

        public static IQueryable<TEntity> WhereByStringWithExist<TEntity>(this IQueryable<TEntity> query,
          string? condition = null)
           where TEntity : class
        {
            string existCondition = "e => e.IsDeleted = false";

            ArgumentNullException.ThrowIfNull(query, nameof(query));

            if (!string.IsNullOrEmpty(condition))
            {
                return query.Where(existCondition + "&&" + condition);
            }

            return query.Where(existCondition);
        }

        /// <summary>
        /// Formmate (fieldNname:(asc|desc))
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderByString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IQueryable<TEntity> OrderByWithFormmat<TEntity>(this IQueryable<TEntity> query, string? orderByString)
        {
            if (string.IsNullOrEmpty(orderByString))
                return query;

            ArgumentNullException.ThrowIfNull(query, nameof(query));

            if (Regex.Match(orderByString, "^\\s*\\w+\\s*:\\s*(asc|desc)\\s*(?:,\\s*\\w+\\s*:\\s*(asc|desc)\\s*)*$\r\n").Success)
                throw new ArgumentException("orderByString is invalid formmat");

            var orderbyStringFmoratted = orderByString.Replace(":", " ");

            return query.OrderBy(orderbyStringFmoratted);
        }

        public static IQueryable<TEntity> IncludeIf<TEntity, TProperty>(
           this IQueryable<TEntity> query,
           bool condition,
           Expression<Func<TEntity, TProperty>> predicate)
        {
            if (condition == true)
            {
                query.Include(predicate);
            }

            return query;
        }

        public static string GetPrimaryKeyName<TEntity>()
            where TEntity : class
        {
            var entityIdName = typeof(TEntity).Name + "Id";
            var primaryKeyProperty = typeof(TEntity).GetProperty(entityIdName);

            if (primaryKeyProperty == null)
            {
                throw new ArgumentException($"Entity {typeof(TEntity).Name} has not Id property");
            }

            return primaryKeyProperty.Name;
        }

        public static IQueryable<TResult> SelectWithField<TEntity, TResult>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, TResult>>? selector = null)
            where TEntity : class
            where TResult : class
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            if (selector == null)
                return query.ProjectToType<TResult>();

            return query.Select(selector);
        }

        public static async Task<IPagedList<TResult>> ToPagedListAsync<TEntity, TResult>(this IQueryable<TEntity> query,
           IPagingQuery<TResult> pagingQuery)
           where TEntity : class
           where TResult : class
        {
            var pagedList = new PagedList<TResult>();
            var resultQuery = query.ProjectToType<TResult>();
            await pagedList.LoadDataAsync(resultQuery, pagingQuery);
            return pagedList;
        }

        public static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> query,
            IPagingQuery<TEntity> pagingQuery)
            where TEntity : class
        {
            pagingQuery ??= new PagingQuery<TEntity>();
            var pagedList = new PagedList<TEntity>();
            await pagedList.LoadDataAsync(query, pagingQuery);
            return pagedList;
        }

        #region Private Methods
        private static Expression<Func<TEntity, bool>> AddExistCondition<TEntity>(this Expression<Func<TEntity, bool>>? filter)
            where TEntity : class
        {
            PropertyInfo? isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty == null)
                throw new ArgumentNullException($"Entity {typeof(TEntity).Name} has not IsDeleted property");

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "isDeleted");
            MemberExpression isDeletedPropertyAccess = Expression.Property(parameter, isDeletedProperty);
            ConstantExpression isDeleted_is_false = Expression.Constant(false);
            BinaryExpression equalityExpression = Expression.Equal(isDeletedPropertyAccess, isDeleted_is_false);
            Expression<Func<TEntity, bool>> isNotDeleteCondition = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameter);

            return filter == null
                ? isNotDeleteCondition
                : isNotDeleteCondition.And(filter);
        }
        #endregion 
    }
}
