using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTree
{
    public static class ExpressionTreeCreating
    {
        private static Dictionary<char, Func<Expression, Expression, Expression>> mas =
            new Dictionary<char, Func<Expression, Expression, Expression>>
                {
                    { '+', (current, next) => Expression.Add(current, next) },
                    { '-', (current, next) => Expression.Subtract(current, next) },
                    { '*', (current, next) => Expression.Multiply(current, next) },
                    { '/', (current, next) => Expression.Divide(current, next) }
                };

        public static decimal Parsing(string expression)
        {
            foreach (var operation in mas)
            {
                if (expression.Contains(operation.Key))
                {
                    var parts = expression.Split(operation.Key);
                    Expression result = Expression.Constant(Parsing(parts[0]));

                    for (int i = 1; i < parts.Length; i++)
                    {
                        result = operation.Value(result, Expression.Constant(Parsing(parts[i])));
                    }

                    var lambda = Expression.Lambda<Func<decimal>>(result);
                    var compiled = lambda.Compile();
                    return compiled();
                }
            }

            decimal value = 0;
            decimal.TryParse(expression, out value);
            return value;
        }
    }
}
