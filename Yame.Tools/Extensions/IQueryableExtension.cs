using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Yame.Tools.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> ToPagedList<TEntity>(
            this IQueryable<TEntity> source,
            string orderByProperty,
            bool desc,
            string defaultOrderByProperty,
            int? page,
            int? limit)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty ?? defaultOrderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            source = source.Provider.CreateQuery<TEntity>(resultExpression);
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                source = source.Skip(start).Take(limit.Value);
            }
            return source;
        }
    }
}
