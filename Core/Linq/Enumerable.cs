using System.Collections.Generic;
using System.Collections;

namespace VariableIrony.Linq
{
    internal class Enumerable<T> : IOuterEnumerable<T>, IInnerEnumerable<T>
    {
        private readonly IEnumerable<T> _source;

        public Enumerable(IEnumerable<T> source)
        {
            this._source = source;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> enumerator = this._source.GetEnumerator();
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerator enumerator = this.GetEnumerator();
            return enumerator;
        }

        public IEnumerable<T> Source
        {
            get
            {
                return this._source;
            }
        }
    }
}
