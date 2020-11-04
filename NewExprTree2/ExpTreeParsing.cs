using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExprTree2
{
   

    public class ExpTreeParsing
    {
        private static Stack<Expression> expList = new Stack<Expression>();//Constant
        private static Stack<ConstantExpression> opExpList = new Stack<ConstantExpression>();//MakeTree
        private IResponcing resp;
        public ExpTreeParsing(IResponcing resp)
        {
            this.resp = resp;
        }
        public async Task<double> CalculateAsync(Expression Tree)
        { 
            VisitorMyTree visitorMy = new VisitorMyTree(resp);
            visitorMy.Visit(Tree);
            return await visitorMy.RunCalculate(Tree);
        }
        public Expression ParsingExpression(char[] exp)
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



        private void ProcessClosingParenthesis()
        {
            while (Convert.ToChar(opExpList.Peek().Value) != '(')
                ExecuteOperation();
            opExpList.Pop(); // Remove the opening parenthesis
        }

        private int ProcessInputNumber(char[] exp, int pos)
        {
            double value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (double)(exp[pos++] - '0');

            expList.Push(Expression.Constant(value));

            return pos;

        }

        private void ProcessInputOperator(char op)
        {
            while (opExpList.Count > 0 &&
                    OperatorCausesEvaluation(op, Convert.ToChar(opExpList.Peek().Value)))
                ExecuteOperation();

            opExpList.Push(Expression.Constant(op));

        }

        private bool OperatorCausesEvaluation(char op,char prevOp)
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
        private void ExecuteOperation()
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
                
                case '+': result = Expression.MakeBinary(ExpressionType.Add, leftOperand, rightOperand);break;
                case '-': result = Expression.MakeBinary(ExpressionType.Subtract, leftOperand, rightOperand); break;
                case '*': result = Expression.MakeBinary(ExpressionType.Multiply, leftOperand, rightOperand); break;
                case '/': result = Expression.MakeBinary(ExpressionType.Divide, leftOperand, rightOperand); break;
                default: throw new ArgumentException();
            }
            expList.Push(result);
        }
    }
}
