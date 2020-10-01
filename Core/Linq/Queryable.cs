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

        public Type ElementType => this.Source.ElementType;
        public Expression Expression => this.Source.Expression;
        public IQueryProvider Provider => this.Source.Provider;
        public new IQueryable<T> Source => (IQueryable<T>)base.Source;
    }
}
