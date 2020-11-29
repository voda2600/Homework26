using System;
using System.Collections.Generic;

namespace FProxyC.CalculatorServ
{
    public class Calc:ICalc
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
                return CalcOperations[c](a, b);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

    }
}
