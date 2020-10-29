using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExprTree2
{
    class VisitorMyTree : ExpressionVisitor
    {
        public double doubleAns;
        public ConcurrentDictionary<string, Task<double>> tasks = new ConcurrentDictionary<string, Task<double>>();

        public override Expression Visit(Expression node)
        {
            var task = Task.Run(async () =>
            {
                double a1 = 0;
                double.TryParse(node.ToString(), out a1);
                if (a1 != 0 || (node.ToString()=="0"))
                    return a1;
                Expression a = ((BinaryExpression)node).Left;
                Expression b = ((BinaryExpression)node).Right;
                Visit((Expression)((ConstantExpression)a).Value);
                Visit((Expression)((ConstantExpression)b).Value);
                var ans = await Task.WhenAll(tasks[a.ToString()], tasks[b.ToString()]);
                object[] args = new object[] {Expression.Constant(ans[0]),Expression.Constant(ans[1])};
                double globAns = (double)((BinaryExpression)node).Method.Invoke(this,args);
                doubleAns = globAns;
                return globAns;
            });
            tasks[node.ToString()] = task;
            return node;
        }
        public Task<double> RunCalculate(Expression Tree)
        {
           return this.tasks[Tree.ToString()];
        }
    }
}
