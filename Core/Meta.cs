using System;
using System.Linq.Expressions;
using System.Reflection;

namespace VariableIrony
{
    public static class Meta
    {
        public static MemberInfo MemberOf<T>(Expression<Func<T>> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(NameOf(() => exp));
            }
            return MemberOf(exp.Body);
        }

        public static MemberInfo MemberOf(Expression<Action> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(NameOf(() => exp));
            }
            return MemberOf(exp.Body);
        }

        private static MemberInfo MemberOf(Expression exp)
        {
            var expression = exp as MemberExpression;
            if (expression != null)
            {
                return expression.Member;
            }
            var expression2 = exp as MethodCallExpression;
            if (expression2 == null)
            {
                throw new ArgumentException("'" + exp + "' does not represent a member access.", "exp");
            }
            return expression2.Method;
        }

        public static string NameOf<T>(Expression<Func<T>> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(NameOf(() => exp));
            }
            return MemberOf(exp.Body).Name;
        }

        public static string NameOf(Expression<Action> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(NameOf(() => exp));
            }
            return MemberOf(exp.Body).Name;
        }
    }
}
