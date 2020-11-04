using ExprTree2;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestZero()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IResponcing, Calc>(); //Responcing or Calc
            var servicesBuild = services.BuildServiceProvider();
            var d = servicesBuild.GetService<IResponcing>();
            var mas = "1/0".Replace(" ", "").ToCharArray();
            ExpTreeParsing expTree = new ExpTreeParsing(d);
            Expression Tree = expTree.ParsingExpression(mas);
            Assert.Throws<AggregateException>(() => (expTree.CalculateAsync(Tree).Result,Expression.Lambda<Func<double>>(Tree).Compile()()));
        }
        
        [Theory]
        [InlineData("1+2")]
        [InlineData("99-1221")]
        [InlineData("12*12")]
        [InlineData("56/7")]
        [InlineData("(88*9)+12")]
        [InlineData("12+(88*9)/10")]
        [InlineData("1+2+3+4+5/1+(12*12-12/2)")]
        [InlineData("12")]
        [InlineData("0/2")]
        [InlineData("2+2*2")]
        public void Test1(string exp)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IResponcing, Calc>(); //Responcing or Calc
            var servicesBuild = services.BuildServiceProvider();
            var d = servicesBuild.GetService<IResponcing>();
            var mas = exp.Replace(" ", "").ToCharArray();
            ExpTreeParsing expTree = new ExpTreeParsing(d);
            Expression Tree = expTree.ParsingExpression(mas);
            Assert.Equal(expTree.CalculateAsync(Tree).Result, Expression.Lambda<Func<double>>(Tree).Compile()());
        }
    }
}
