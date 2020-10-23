using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace ExprTree2
{
    public static class ExpTreeParsing
    {
        public static Stack<Expression> expList = new Stack<Expression>();
        public static Stack<ConstantExpression> opExpList = new Stack<ConstantExpression>();
        public static void EvaluateExpression(char[] exp)
        {

           
           
            opExpList.Push(Expression.Constant('('));

            int pos = 0;
            while (pos <= exp.Length)
            {
                if (pos == exp.Length || exp[pos] == ')')
                {
                    ProcessClosingParenthesis();
                    pos++;
                }
                else if (exp[pos] >= '0' && exp[pos] <= '9')
                {
                    pos = ProcessInputNumber(exp, pos);
                }
                else
                {
                    ProcessInputOperator(exp[pos]);
                    pos++;
                }
                foreach (var j in expList)
                    Console.Write(j + ",");
                Console.WriteLine();
                foreach (var k in opExpList)
                    Console.Write(k + ",");
                Console.WriteLine("\n/////////////////////////////////////////////////");
            }
            Console.WriteLine("ВНИМАНИЕ, что в EXPLIST:");
            foreach (var h in expList)
                Console.WriteLine(h);
            Console.WriteLine("ЗАКОНЧИЛ");
            var ans=expList.Pop();
            var res = Expression.Lambda<Func<double>>(ans).Compile()();
            Console.WriteLine(res);
             // Result remains on values stacks

        }



        public static void ProcessClosingParenthesis()
        {
            while (Convert.ToChar(opExpList.Peek().Value) != '(')
                ExecuteOperation();

            opExpList.Pop(); // Remove the opening parenthesis

        }






        public static int ProcessInputNumber(char[] exp, int pos)
        {

            int value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (int)(exp[pos++] - '0');

            expList.Push(Expression.Constant(value));

            return pos;

        }






        public static void ProcessInputOperator(char op)
        {
            while (opExpList.Count > 0 &&
                    OperatorCausesEvaluation(op, Convert.ToChar(opExpList.Peek().Value)))
                ExecuteOperation();

            opExpList.Push(Expression.Constant(op));

        }




        public static bool OperatorCausesEvaluation(char op,char prevOp)
        {

            bool evaluate = false;

            switch (op)
            {
                case '+':
                case '-':
                    evaluate = (prevOp != '(');
                    break;
                case '*':
                case '/':
                    evaluate = (prevOp == '*' || prevOp == '/');
                    break;
                case ')':
                    evaluate = true;
                    break;
            }

            return evaluate;

        }



        public static void ExecuteOperation()
        {

            var rightOperand = expList.Pop();
            var leftOperand = expList.Pop();
            ConstantExpression op = opExpList.Pop();
            
            BinaryExpression result;
            switch (op.Value)
            {
                case '+': result = Expression.MakeBinary(ExpressionType.Add, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiPlus"));break;
                case '-': result = Expression.MakeBinary(ExpressionType.Subtract, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiMin")); break;
                case '*': result = Expression.MakeBinary(ExpressionType.Multiply, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiMult")); break;
                case '/': result = Expression.MakeBinary(ExpressionType.Divide, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiDel")); break;
                default: result = Expression.MakeBinary(ExpressionType.Call, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiWTF")); break;
            }
            

            expList.Push(result);

        }

    }
    public class Responcing
    {
        public static int chert = 0;
        public static double GetResponsiPlus(Expression a, Expression b)
        {
            
            char oper = '+';
            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "%2B";
            if (a1==0 && a.ToString().Length > 1 && b1!=0)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
            }
            else if (b1 == 0 && b.ToString().Length > 1 && a1!=0)
            {
                b1 = Expression.Lambda<Func<double>>(b).Compile()(); 
            }
            else if(a1==0 && b1 == 0 && b.ToString().Length > 1 && a.ToString().Length > 1)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }
                
            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(" (" + a1.ToString() + " fuck " + oper + " " + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);//decimal.Parse(ans);
        }
        public static double GetResponsiMin(Expression a, Expression b)
        {
            char oper = '-';
            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "-";
            if (a1 == 0 && a.ToString().Length > 1 && b1 != 0)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
            }
            else if (b1 == 0 && b.ToString().Length > 1 && a1 != 0)
            {
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }
            else if (a1 == 0 && b1 == 0 && b.ToString().Length > 1 && a.ToString().Length > 1)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }

            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(" (" + a1.ToString() + " fuck " + oper + " " + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);//decimal.Parse(ans);
        }
        public static double GetResponsiDel(Expression a, Expression b)
        {
            char oper = '/';
            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "%2F";
            if (a1 == 0 && a.ToString().Length > 1 && b1 != 0)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
            }
            else if (b1 == 0 && b.ToString().Length > 1 && a1 != 0)
            {
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }
            else if (a1 == 0 && b1 == 0 && b.ToString().Length > 1 && a.ToString().Length > 1)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }

            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(" (" + a1.ToString() + " fuck " + oper + " " + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);//decimal.Parse(ans);
        }
        public static double GetResponsiMult(Expression a, Expression b)
        {
            char oper = '*';
            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "*";
            if (a1 == 0 && a.ToString().Length > 1 && b1 != 0)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
            }
            else if (b1 == 0 && b.ToString().Length > 1 && a1 != 0)
            {
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }
            else if (a1 == 0 && b1 == 0 && b.ToString().Length > 1 && a.ToString().Length > 1)
            {
                a1 = Expression.Lambda<Func<double>>(a).Compile()();
                b1 = Expression.Lambda<Func<double>>(b).Compile()();
            }

            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(" (" + a1.ToString() + " fuck " + oper + " " + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);//decimal.Parse(ans);
        }

        }
}
