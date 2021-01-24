using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExprTree2
{
    public abstract  class MyExpressionTreeVisitor
    {
        public MyExpressionTreeVisitor() { }
        public virtual Expression Visit(Expression tree)
        {
            if (tree == null)
                return tree;

            dynamic dynamicNode = tree;
            return DynamicVisit(dynamicNode);
        }
        public virtual Expression DynamicVisit(ConstantExpression dynamicNode)
        {
            return dynamicNode;
        }
        public virtual Expression DynamicVisit(BinaryExpression dynamicNode)
        {
            return dynamicNode;
        }
        private Expression DynamicVisit(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
