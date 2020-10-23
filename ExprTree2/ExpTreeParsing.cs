using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ExprTree2
{
    public static class ExpTreeParsing
    {
        public static double EvaluateExpression(char[] exp)
        {

            Stack<double> vStack = new Stack<double>();
            Stack<char> opStack = new Stack<char>();

            opStack.Push('('); 
            int pos = 0;
            while (pos <= exp.Length)
            {
                if (pos == exp.Length || exp[pos] == ')')
                {
                    ProcessClosingParenthesis(vStack, opStack);
                    pos++;
                }
                else if (exp[pos] >= '0' && exp[pos] <= '9')
                {
                    pos = ProcessInputNumber(exp, pos, vStack);
                }
                else
                {
                    ProcessInputOperator(exp[pos], vStack, opStack);
                    pos++;
                }

            }

            return vStack.Pop(); // Result remains on values stacks

        }



        public static void ProcessClosingParenthesis(Stack<double> vStack,
                                        Stack<char> opStack)
        {

            while (opStack.Peek() != '(')
                ExecuteOperation(vStack, opStack);

            opStack.Pop(); // Remove the opening parenthesis

        }






        public static int ProcessInputNumber(char[] exp, int pos,
                                Stack<double> vStack)
        {

            int value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (int)(exp[pos++] - '0');

            vStack.Push(value);

            return pos;

        }






        public static void ProcessInputOperator(char op, Stack<double> vStack,
                                    Stack<char> opStack)
        {

            while (opStack.Count > 0 &&
                    OperatorCausesEvaluation(op, opStack.Peek()))
                ExecuteOperation(vStack, opStack);

            opStack.Push(op);

        }




        public static bool OperatorCausesEvaluation(char op, char prevOp)
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



        public static void ExecuteOperation(Stack<double> vStack,
                                Stack<char> opStack)
        {

            double rightOperand = vStack.Pop();
            double leftOperand = vStack.Pop();
            char op = opStack.Pop();

            double result = Responcing.GetResponsi(leftOperand, rightOperand, op);
            vStack.Push(result);

        }

    }
    public class Responcing
    {
        public static int chert = 0;
        public static double GetResponsi(double a, double b, char oper)
        {

            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            string oper1 = "";
            if (oper.ToString() == "+")
                oper1 = "%2B";
            else if (oper.ToString() == "/")
                oper1 = "%2F";
            else if (oper.ToString() == "-")
                oper1 = "-";
            else if (oper.ToString() == "*")
                oper1 = "*";
            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            for (int j = 0; j < Responcing.chert; j++)
                Console.Write("-");
            Console.WriteLine(" (" + a1.ToString() + " " + oper + " " + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);//decimal.Parse(ans);
        }
    }
}
