using System.Collections.Generic;
using System.Collections;

namespace VariableIrony.Linq
{
    internal class Enumerable<T> : IOuterEnumerable<T>, IInnerEnumerable<T>
    {
        public Enumerable(IEnumerable<T> source)
        {
            this.Source = source;
        }

        public IEnumerable<T> Source { get; }
        public IEnumerator<T> GetEnumerator() => this.Source.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
