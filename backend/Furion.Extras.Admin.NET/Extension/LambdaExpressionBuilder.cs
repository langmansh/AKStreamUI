using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 动态生成查询表达式
    /// </summary>
    public static class LambdaExpressionBuilder
    {
        private static Expression GetExpression(ParameterExpression parameter, Condition condition)
        {
            var propertyParam = Expression.Property(parameter, condition.Field);

            var propertyInfo = propertyParam.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new MissingMemberException(nameof(Condition), condition.Field);

            //Support Nullable<>
            var realPropertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            if (propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propertyParam = Expression.Property(propertyParam, "Value");

            //Support IEnumerable && IEnumerable<T>
            if (condition.Op != QueryTypeEnum.StdIn && condition.Op != QueryTypeEnum.StdNotIn)
            {
                condition.Value = Convert.ChangeType(condition.Value, realPropertyType);
            }
            else
            {
                var typeOfValue = condition.Value.GetType();
                var typeOfList = typeof(IEnumerable<>).MakeGenericType(realPropertyType);
                if (typeOfValue.IsGenericType && typeOfList.IsAssignableFrom(typeOfValue))
                    condition.Value = typeof(Enumerable)
                        .GetMethod("ToArray", BindingFlags.Public | BindingFlags.Static)
                        ?.MakeGenericMethod(realPropertyType)
                        .Invoke(null, new[] { condition.Value });
            }

            var constantParam = Expression.Constant(condition.Value);
            switch (condition.Op)
            {
                case QueryTypeEnum.Equals:
                    return Expression.Equal(propertyParam, constantParam);
                case QueryTypeEnum.NotEquals:
                    return Expression.NotEqual(propertyParam, constantParam);
                case QueryTypeEnum.Contains:
                    return Expression.Call(propertyParam, "Contains", null, constantParam);
                case QueryTypeEnum.NotContains:
                    return Expression.Not(Expression.Call(propertyParam, "Contains", null, constantParam));
                case QueryTypeEnum.StartsWith:
                    return Expression.Call(propertyParam, "StartsWith", null, constantParam);
                case QueryTypeEnum.EndsWith:
                    return Expression.Call(propertyParam, "EndsWith", null, constantParam);
                case QueryTypeEnum.GreaterThan:
                    return Expression.GreaterThan(propertyParam, constantParam);
                case QueryTypeEnum.GreaterThanOrEquals:
                    return Expression.GreaterThanOrEqual(propertyParam, constantParam);
                case QueryTypeEnum.LessThan:
                    return Expression.LessThan(propertyParam, constantParam);
                case QueryTypeEnum.LessThanOrEquals:
                    return Expression.LessThanOrEqual(propertyParam, constantParam);
                case QueryTypeEnum.StdIn:
                    return Expression.Call(typeof(Enumerable), "Contains", new[] { realPropertyType }, constantParam, propertyParam);
                case QueryTypeEnum.StdNotIn:
                    return Expression.Not(Expression.Call(typeof(Enumerable), "Contains", new[] { realPropertyType }, constantParam, propertyParam));
                default:
                    break;
            }

            return null;
        }

        private static Expression GetGroupExpression(ParameterExpression parameter, List<Condition> orConditions)
        {
            if (orConditions.Count == 0)
                return null;

            var exps = orConditions.Select(c => GetExpression(parameter, c)).ToList();
            return exps.Aggregate<Expression, Expression>(null, (left, right) =>
                left == null ? right : Expression.OrElse(left, right));
        }

        public static Expression<Func<T, bool>> BuildLambda<T>(IEnumerable<Condition> conditions)
        {
            if (conditions == null || !conditions.Any())
                return x => true;

            var parameter = Expression.Parameter(typeof(T), "x");

            //简单条件
            var simpleExps = conditions
                .ToList()
                .FindAll(c => string.IsNullOrEmpty(c.OrGroup))
                .Select(c => GetExpression(parameter, c))
                .ToList();

            //复杂条件
            var complexExps = conditions
                .ToList()
                .FindAll(c => !string.IsNullOrEmpty(c.OrGroup))
                .GroupBy(x => x.OrGroup)
                .Select(g => GetGroupExpression(parameter, g.ToList()))
                .ToList();

            var exp = simpleExps.Concat(complexExps).Aggregate<Expression, Expression>(null, (left, right) =>
                left == null ? right : Expression.AndAlso(left, right));
            return Expression.Lambda<Func<T, bool>>(exp, parameter);
        }

        public static Expression<Func<T, bool>> BuildAndAlsoLambda<T>(IEnumerable<Condition> conditions)
        {
            if (conditions == null || !conditions.Any())
                return x => true;

            var parameter = Expression.Parameter(typeof(T), "x");
            var simpleExps = conditions
                .ToList()
                .Select(c => GetExpression(parameter, c))
                .ToList();

            var exp = simpleExps.Aggregate<Expression, Expression>(null, (left, right) =>
                left == null ? right : Expression.AndAlso(left, right));
            return Expression.Lambda<Func<T, bool>>(exp, parameter);

        }

        public static Expression<Func<T, bool>> BuildOrElseLambda<T>(IEnumerable<Condition> conditions)
        {
            if (conditions == null || !conditions.Any())
                return x => true;

            var parameter = Expression.Parameter(typeof(T), "x");
            var simpleExps = conditions
                .ToList()
                .Select(c => GetExpression(parameter, c))
                .ToList();

            var exp = simpleExps.Aggregate<Expression, Expression>(null, (left, right) =>
                left == null ? right : Expression.OrElse(left, right));
            return Expression.Lambda<Func<T, bool>>(exp, parameter);
        }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable]
    public class Condition
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public QueryTypeEnum Op { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string OrGroup { get; set; }
    }
}