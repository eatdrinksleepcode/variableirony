using System;
using System.Linq;
using System.Linq.Expressions;

namespace VariableIrony.Linq
{
    internal class Queryable<T> : Enumerable<T>, IOuterQueryable<T>, IInnerQueryable<T>
    {
        public Queryable(IQueryable<T> source)
            : base(source)
        {
        }

        public Type ElementType
        {
            get
            {
                return this.Source.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this.Source.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.Source.Provider;
            }
        }

        public new IQueryable<T> Source
        {
            get
            {
                return (IQueryable<T>)base.Source;
            }
        }
    }
}
