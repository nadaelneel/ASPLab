using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class IQuerableExtention
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source , bool isAcending , string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                return source;
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");
            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);
            string methodName = isAcending ? "OrderBy" : "OrderByDescending";
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
