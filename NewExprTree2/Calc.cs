using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExprTree2
{
    public class Calc:IResponcing
    {
        private Dictionary<char, Func<double, double, double>> CalcOperations = new Dictionary<char, Func<double, double, double>>();
        public Calc()
        {
            CalcOperations.Add('+', (x, y) => x + y);
            CalcOperations.Add('-', (x, y) => x - y);
            CalcOperations.Add('*', (x, y) => x * y);
            CalcOperations.Add('/', (x, y) => { if (y == 0)  throw new DivideByZeroException(); else return (x / y); });
        }
        public double Calculate(double a, char c, double b)
        {
            try
            {
                double ans = CalcOperations[c](a, b);
                return ans;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public double GetPesponsing(double a1, double b1, string oper1)
        {
            double ans = Calculate(a1, char.Parse(oper1), b1);
            Console.WriteLine("Считаю на локальном калькуляторе (" + a1.ToString() + oper1 + b1.ToString() + " = " + ans + ") ");
            return ans;
        }

    }
}
