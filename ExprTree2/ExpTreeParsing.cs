using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace ExprTree2
{
   

    public static class ExpTreeParsing
    {
        public static Stack<Expression> expList = new Stack<Expression>();//Constant
        public static Stack<ConstantExpression> opExpList = new Stack<ConstantExpression>();//MakeTree
        public static double Calculate(Expression Tree)
        { 
            VisitorMyTree visitorMy = new VisitorMyTree();
            visitorMy.Visit(Tree);
            return double.Parse(visitorMy.DoubleAns);
        }
        public static Expression ParsingExpression(char[] exp)
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
            }
             return expList.Pop();
        }



        private static void ProcessClosingParenthesis()
        {
            while (Convert.ToChar(opExpList.Peek().Value) != '(')
                ExecuteOperation();
            opExpList.Pop(); // Remove the opening parenthesis
        }

        private static int ProcessInputNumber(char[] exp, int pos)
        {

            int value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (int)(exp[pos++] - '0');

            expList.Push(Expression.Constant(value));

            return pos;

        }

        private static void ProcessInputOperator(char op)
        {
            while (opExpList.Count > 0 &&
                    OperatorCausesEvaluation(op, Convert.ToChar(opExpList.Peek().Value)))
                ExecuteOperation();

            opExpList.Push(Expression.Constant(op));

        }

        private static bool OperatorCausesEvaluation(char op,char prevOp)
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

        //Создаётся дерево
        private static void ExecuteOperation()
        {
            Expression rightOperand;
            Expression leftOperand;
            try
            {
                 rightOperand = expList.Pop();
                 leftOperand = expList.Pop();
            }
            catch
            {
                throw new Exception("Строка имеет не верный формат");
            }
            ConstantExpression op = opExpList.Pop();
            BinaryExpression result;
            switch (op.Value)
            {
                case '+': result = Expression.MakeBinary(ExpressionType.Add, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiPlus"));break;
                case '-': result = Expression.MakeBinary(ExpressionType.Subtract, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiMin")); break;
                case '*': result = Expression.MakeBinary(ExpressionType.Multiply, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiMult")); break;
                case '/': result = Expression.MakeBinary(ExpressionType.Divide, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Responcing).GetMethod("GetResponsiDel")); break;
                default: throw new ArgumentException();
            }
            expList.Push(result);
        }
    }
}
