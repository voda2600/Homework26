using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace ExpressionTree
{   public  class Responcing
    {
        public static int chert = 0;
        public static double GetResponsi(Expression a, Expression b,Expression oper) 
        {
            chert++;
            double a1= 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "";
            if (oper.ToString() == "+")
                oper1 = "%2B";
            else if (oper.ToString() == "/")
                oper1 = "%2F";
            else if (oper.ToString() == "-")
                oper1 = "-";
            else if (oper.ToString() == "*")
                oper1 = "*";
            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(a1.ToString() + " " + oper + " " + b1.ToString() + " = " + ans) ;
            return double.Parse(ans);//decimal.Parse(ans);
        }
    }
    public class ExpressionTreeCreating
    {   
        
        private static Dictionary<char, Func<Expression, Expression, Expression,Expression>> mas =
            new Dictionary<char, Func<Expression, Expression, Expression,Expression>>
                {
                    { '+', (current, next,oper) => Expression.Call(typeof(Responcing)
                                              .GetMethod("GetResponsi")
                                              ,Expression.Constant(current)
                                              ,Expression.Constant(next)
                                              ,Expression.Constant(oper))},
                    {'-', (current, next,oper) => Expression.Call(typeof(Responcing)
                                              .GetMethod("GetResponsi")
                                              ,Expression.Constant(current)
                                              ,Expression.Constant(next)
                                              ,Expression.Constant(oper))},
                    { '*', (current, next,oper) => Expression.Call(typeof(Responcing)
                                              .GetMethod("GetResponsi")
                                              ,Expression.Constant(current)
                                              ,Expression.Constant(next)
                                              ,Expression.Constant(oper))},
                    { '/', (current, next,oper) => Expression.Call(typeof(Responcing)
                                              .GetMethod("GetResponsi")
                                              ,Expression.Constant(current)
                                              ,Expression.Constant(next)
                                              ,Expression.Constant(oper))},
                };

        public static double Parsing(string expression)
        {
            foreach (var operation in mas)
            {
                if (expression.Contains(operation.Key))
                {
                    double res = 0;
                    var parts = expression.Split(operation.Key);
                    Expression result = Expression.Constant(Parsing(parts[0]));

                    for (int i = 1; i < parts.Length; i++)
                    {
                        res = Expression.Lambda<Func<double>>(result).Compile()();
                       
                        result = (MethodCallExpression)operation.Value(Expression.Constant(res), Expression.Constant(Parsing(parts[i])),Expression.Constant(operation.Key));
                       
                        
                    }

                    var lambda = Expression.Lambda<Func<double>>(result);
                    var compiled = lambda.Compile();
                    return compiled();
                }
            }

            double value = 0;
            double.TryParse(expression, out value);
            return value;
        }
    }
}
