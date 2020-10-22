using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(ExpressionTreeCreating.Parsing("9*2-5/2+1/4-2/8+9/10"));
        }
    }
}
