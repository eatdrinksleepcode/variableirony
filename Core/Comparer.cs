using System;

namespace VariableIrony
{
    public class Comparer<T> : System.Collections.Generic.Comparer<T>
    {
        private readonly Comparison<T> _comparison;

        public Comparer(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        public override int Compare(T x, T y)
        {
            return _comparison(x, y);
        }
    }
}
