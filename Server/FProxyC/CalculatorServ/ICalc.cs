using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FProxyC.CalculatorServ
{
    public interface ICalc
    {
         double Calculate(string expression); 
    }
}
