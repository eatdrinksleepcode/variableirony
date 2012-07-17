using System.Linq;

namespace VariableIrony.Linq
{
    /// <summary>
    /// Marks a queryable sequence as being an outer side of an outer join.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <remarks>Marking a sequence as an outer side is useful in selecting the correct Join overload when executing an outer join in a query expression.</remarks>
    /// <seealso cref="M:VariableIrony.IQueryableExtensions.AsOuter``1(System.Linq.IQueryable{``0})" />.
    public interface IOuterQueryable<out T> : IQueryable<T>, IOuterEnumerable<T>
    {
    }
}
