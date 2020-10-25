using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExprTree2
{
    class VisitorMyTree : ExpressionVisitor
    {
        public string DoubleAns = "";
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression a = node.Left;
            Expression b = node.Right;
            double a1 = 0;
            double.TryParse(a.ToString(), out a1);
            double b1 = 0;
            double.TryParse(b.ToString(), out b1);
            Expression a2 = Expression.Constant(a1);
            Expression b2 = Expression.Constant(b1);
            if (a.Type.Name == "MethodBinaryExpression" && b.Type.Name != "MethodBinaryExpression")
            {
                a2 = this.VisitBinary((BinaryExpression)((ConstantExpression)a).Value);
            }
            else if (b.Type.Name == "MethodBinaryExpression" && a.Type.Name != "MethodBinaryExpression")
            {
                b2 = this.VisitBinary((BinaryExpression)((ConstantExpression)b).Value);
            }
            else if (a.Type.Name == "MethodBinaryExpression" && b.Type.Name == "MethodBinaryExpression")
            {
                ConstantExpression f = (ConstantExpression)a;
                    BinaryExpression f1 = (BinaryExpression)f.Value;
                a2 = this.VisitBinary((BinaryExpression)((ConstantExpression)a).Value);
                b2 = this.VisitBinary((BinaryExpression)((ConstantExpression)b).Value);
            }

            Expression globAns;
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    globAns = Expression.Constant(Responcing.GetResponsiPlus(a2, b2));
                    break;
                case ExpressionType.Multiply:
                    globAns = Expression.Constant(Responcing.GetResponsiMult(a2, b2));
                    break;
                case ExpressionType.Divide:
                    globAns = Expression.Constant(Responcing.GetResponsiDel(a2, b2));
                    break;
                case ExpressionType.Subtract:
                    globAns = Expression.Constant(Responcing.GetResponsiMin(a2, b2));
                    break;
                default: throw new ArgumentException(); 
            }
            DoubleAns = globAns.ToString();
            return globAns;
        }
    }
}
