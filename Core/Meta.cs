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
                throw new ArgumentNullException(nameof(exp));
            }
            return MemberOf(exp.Body);
        }

        public static MemberInfo MemberOf(Expression<Action> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(nameof(exp));
            }
            return MemberOf(exp.Body);
        }

        private static MemberInfo MemberOf(Expression exp)
        {
            switch (exp)
            {
                case MemberExpression expMember:
                    return expMember.Member;
                case MethodCallExpression expCall:
                    return expCall.Method;
                default:
                    throw new ArgumentException($"'{exp}' does not represent a member access.", nameof(exp));
            }
        }

        public static string NameOf<T>(Expression<Func<T>> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(nameof(exp));
            }
            return MemberOf(exp.Body).Name;
        }

        public static string NameOf(Expression<Action> exp)
        {
            if (exp == null)
            {
                throw new ArgumentNullException(nameof(exp));
            }
            return MemberOf(exp.Body).Name;
        }
    }
}
