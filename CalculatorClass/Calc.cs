using System;
using System.Collections.Generic;

namespace CalculatorClass
{
    public class Calc
    {
        private Dictionary<char, Func<double, double, double>> CalcOperations = new Dictionary<char, Func<double, double, double>>();
        public Calc()
        {
            CalcOperations.Add('+', (x, y) => x + y);
            CalcOperations.Add('-', (x, y) => x - y);
            CalcOperations.Add('*', (x, y) => x * y);
            CalcOperations.Add('/', (x, y) => { if (y == 0) throw new DivideByZeroException(); else return (x / y); });
        }
        public double Calculate(double a, char c, double b)
        {
            return CalcOperations[c](a, b);
        }
    }
}
