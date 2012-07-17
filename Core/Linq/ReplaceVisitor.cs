using System.Linq.Expressions;

namespace VariableIrony.Linq
{
    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression _from;
        private readonly Expression _to;

        private ReplaceVisitor(Expression from, Expression to)
        {
            this._from = from;
            this._to = to;
        }

        public static Expression Replace(Expression target, Expression from, Expression to)
        {
            return new ReplaceVisitor(from, to).Visit(target);
        }

        public override Expression Visit(Expression node)
        {
            if (node != this._from)
            {
                return base.Visit(node);
            }
            return this._to;
        }
    }
}
