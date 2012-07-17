using System.Linq;

namespace VariableIrony.Linq
{
    /// <summary>
    /// Marks a queryable sequence as being the inner side of an outer join.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <remarks>Marking a sequence as an inner side is useful in selecting the correct Join overload when executing an outer join in a query expression.</remarks>
    /// <seealso cref="M:VariableIrony.IQueryableExtensions.AsInner``1(System.Linq.IQueryable{``0})" />.
    public interface IInnerQueryable<out T> : IQueryable<T>, IInnerEnumerable<T>
    {
    }
}
