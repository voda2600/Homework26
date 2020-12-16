using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FProxyC.Middleware;
using FProxyC.CalculatorServ;

namespace FProxyC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICalc, Calc>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ICalc calc)
        {
            // + в GET запросах это %2B
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseRouting();
            app.UseCalculator(calc);
            app.UseEndpoints(endpoints =>
            {
                app.Run( async context =>
                {
                    await context.Response.WriteAsync("Parameters for calculator not found!");
                });
            });
            

        }
    }
}
