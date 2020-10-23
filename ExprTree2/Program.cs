using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ExprTree2
{
    class Program
    {
        static void Main(string[] args)
        {
            //"(2+3)/12*7+8*9"
            var mas = "(2+3)/12*7+8*9".ToCharArray();
            ExpTreeParsing.EvaluateExpression(mas);
            
        }
    }
}
