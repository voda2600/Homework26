using FProxyC.CalculatorServ;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FProxyC.Middleware
{
    public static class GetMiddleware
    {
        public static IApplicationBuilder UseCalculator(this IApplicationBuilder builder,ICalc calc)
        {
            return builder.UseMiddleware<CalcMiddle>(calc);
        }
    }
}
