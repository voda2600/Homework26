using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ExpressionTreeCreating.Parsing("2+3*10"));
        }
    }
}
