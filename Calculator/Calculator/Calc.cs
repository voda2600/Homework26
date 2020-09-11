using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class Calc
    {
        public static double Calculate(double a, char c, double b)
        {
            double ans = 0;
            switch (c)
            {
                case '+': ans = a + b;break;
                case '-': ans = a - b;break;
                case '*': ans = a * b; break;
                case '/': 
                        {
                        if (b == 0) throw new DivideByZeroException();
                        else {
                            ans = (double)a / b;
                            break;
                            }
                        }
                default:
                    throw new Exception("Калькулятор не может работать с таким выражением");
            }
            return ans;

        }
    }
}

