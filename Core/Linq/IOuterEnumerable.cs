using System.Collections.Generic;

namespace VariableIrony.Linq
{
    /// <summary>
    /// Marks a sequence as being an outer side of an outer join.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <remarks>Marking a sequence as an outer side is useful in selecting the correct Join overload when executing an outer join in a query expression.</remarks>
    /// <seealso cref="M:VariableIrony.IEnumerableExtensions.AsOuter``1(System.Collections.Generic.IEnumerable{``0})" />.
    public interface IOuterEnumerable<out T> : IEnumerable<T>
    {
    }
}
