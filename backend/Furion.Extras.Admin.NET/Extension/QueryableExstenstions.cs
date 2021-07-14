using Furion.LinqBuilder;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Furion.Extras.Admin.NET
{
    public static class QueryableExstenstions
    {
        /// <summary>
        /// 根据分页查询参数为IQueryable附加过滤条件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="searchParameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> Search<T>(this IQueryable<T> source, PageInputBase searchParameters)
        {
            if (searchParameters.SearchParameters == null)
                return source;

            var results = source.Where(LambdaExpressionBuilder.BuildLambda<T>(searchParameters.SearchParameters));

            //无排序字段
            if (searchParameters.SortField.IsNullOrEmpty())
                return results;

            return results.ApplyOrder(searchParameters.SortField, searchParameters.SortOrder);
        }

        /// <summary>
        /// 附加排序
        /// </summary>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <param name="sortMethod"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, string sortMethod)
        {
            var type = typeof(T);
            var parameterExp = Expression.Parameter(type, "x");
            var propertyInfo = type.GetProperty(property);
            var propertyExp = Expression.Property(parameterExp, propertyInfo);
            var lambdaExp = Expression.Lambda<Func<T, dynamic>>(propertyExp, parameterExp);

            return sortMethod == "descend" ? source.OrderByDescending(lambdaExp) : source.OrderBy(lambdaExp);
        }
    }
}