using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FProxyC.CalculatorServ
{
    public class CalcMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ICalc calc;
        public CalcMiddle(RequestDelegate next, ICalc calc)
        {
            this._next = next;
            this.calc = calc;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Query.ContainsKey("a") && context.Request.Query.ContainsKey("oper") &&
                        context.Request.Query.ContainsKey("b")))
                await _next.Invoke(context);
            else
            {
                string ans = "";
                if (context.Request.Query["oper"] == "/" ||
                    context.Request.Query["oper"] == "+" ||
                    context.Request.Query["oper"] == "-" ||
                    context.Request.Query["oper"] == "*")
                    ans = calc.Calculate(double.Parse(context.Request.Query["a"]), char.Parse(context.Request.Query["oper"]), double.Parse(context.Request.Query["b"])).ToString();
                else ans = "No found this operation in calculator";
                await context.Response.WriteAsync(ans);
            }
        }
    }
}
