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

            double ans = Calculator.Calc.Calculate(a,c,b);
            Assert.AreEqual(expected, ans); 

        }
        [TestMethod]
        public void TestDef10and20_ansM10()
        {
            double a = 10;
            char c = '-';
            double b = 20;
            double expected = -10;

            double ans = Calculator.Calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans);

        }
        [TestMethod]
        public void TestDef1000010and1000()
        {
            double a = 10000.10;
            char c = '-';
            double b = 1000;
            double expected = 9000.10;

            double ans = Calculator.Calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans);

        }
        [TestMethod]
        public void TestMulti99and2_ans198()
        {
            double a = 99;
            char c = '*';
            double b = 2;
            double expected = 198;

            double ans = Calculator.Calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans);

        }
        [TestMethod]
        public void TestDelen9999and9999_ans1()
        {
            double a = 9999;
            char c = '/';
            double b = 9999;
            double expected = 1;

            double ans = Calculator.Calc.Calculate(a, c, b);
            Assert.AreEqual(expected, ans);

        }
        [TestMethod]
        public void TestDelen9999and0_ansExc()
        {
            double a = 9999;
            char c = '/';
            double b = 0;

            Assert.ThrowsException<System.DivideByZeroException>(() => Calculator.Calc.Calculate(a, c, b));

        }
    }
}
