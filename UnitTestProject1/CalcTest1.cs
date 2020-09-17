using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CalcTest1
    {
        [TestMethod]
        public void TestSum10and20_ans30()
        {
            double a = 10;
            char c = '+';
            double b = 20;
            double expected = 30;
            CalculatorClass.Calc calc = new CalculatorClass.Calc();
            double ans = calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans); 

        }
       
        [TestMethod]
        public void TestDelen9999and9999_ans1()
        {
            double a = 9999;
            char c = '/';
            double b = 9999;
            double expected = 1;
            CalculatorClass.Calc calc = new CalculatorClass.Calc();
            double ans = calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans);

        }
        [TestMethod]
        public void TestDelen9999and0_ansExc()
        {
            double a = 9999;
            char c = '/';
            double b = 0;
            CalculatorClass.Calc calc = new CalculatorClass.Calc();

            Assert.ThrowsException<System.DivideByZeroException>(() => calc.Calculate(a, c, b));

        }
        
     
    }
}
