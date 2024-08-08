
using System.Data.Entity;
using LinqKit;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Domain.Common.PagedList;
using Mapster;
using Domain.Common.Queries.Interface;

namespace Infrastructure.Common
{
    internal static class EntityFrameworlExtensions
    {
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


    }
}
