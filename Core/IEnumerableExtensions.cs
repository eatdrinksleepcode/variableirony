using System;
using System.Collections.Generic;
using System.Linq;
using VariableIrony.Linq;

namespace VariableIrony {
	public static class IEnumerableExtensions {

        /// <summary>
        /// Appends an element to the end of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">The sequence to which the item should be appended.</param>
        /// <param name="value">The item to append.</param>
        /// <returns>A new sequence that consists of the original sequence, followed by the appended item.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <seealso cref="M:System.Linq.Enumerable.Concat``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0})" />
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T value)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.Concat(Enumerable.Repeat(value, 1));
        }

        /// <summary>
        /// Returns the input typed as <see cref="T:VariableIrony.Linq.IInnerEnumerable`1" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The sequence to type as <see cref="T:VariableIrony.Linq.IInnerEnumerable`1" />.</param>
        /// <returns>The input sequence typed as <see cref="T:VariableIrony.Linq.IInnerEnumerable`1" />.</returns>
        /// <remarks>This method can be called inside a query expression to mark a sequence as the inner side of an outer join.</remarks>
        /// <seealso cref="M:VariableIrony.IEnumerableExtensions.Join``4(System.Collections.Generic.IEnumerable{``0},VariableIrony.Linq.IInnerEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3})" />
        public static IInnerEnumerable<T> AsInner<T>(this IEnumerable<T> source)
        {
            return new Enumerable<T>(source);
        }

        /// <summary>
        /// Returns the input typed as <see cref="T:VariableIrony.Linq.IOuterEnumerable`1" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The sequence to type as <see cref="T:VariableIrony.Linq.IOuterEnumerable`1" />.</param>
        /// <returns>The input sequence typed as <see cref="T:VariableIrony.Linq.IOuterEnumerable`1" />.</returns>
        /// <remarks>This method can be called inside a query expression to mark a sequence as an outer side of an outer join.</remarks>
        /// <seealso cref="M:VariableIrony.IEnumerableExtensions.Join``4(System.Collections.Generic.IEnumerable{``0},VariableIrony.Linq.IOuterEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3})" />
        public static IOuterEnumerable<T> AsOuter<T>(this IEnumerable<T> source)
        {
            return new Enumerable<T>(source);
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionMessage">The exception message to throw if the sequence is empty.</param>
        public static TSource First<TSource>(this IEnumerable<TSource> source, string exceptionMessage)
        {
            TSource local;
            if (!source.TryFirst(out local))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionMessage">The exception message to throw if the sequence is empty.</param>
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string exceptionMessage)
        {
            TSource local;
            if (!source.TryFirst(predicate, out local))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionFormat">The format string of the custom exception message to throw if the sequence is empty.</param>
        /// <param name="args">The arguments to format.</param>
        public static TSource First<TSource>(this IEnumerable<TSource> source, string exceptionFormat, params object[] args)
        {
            TSource local;
            if (!source.TryFirst(out local))
            {
                throw new InvalidOperationException(String.Format(exceptionFormat, args));
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionFormat">The format string of the custom exception message to throw if the sequence is empty.</param>
        /// <param name="args">The arguments to format.</param>
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string exceptionFormat, params object[] args)
        {
            TSource local;
            if (!source.TryFirst(predicate, out local))
            {
                throw new InvalidOperationException(String.Format(exceptionFormat, args));
            }
            return local;
        }

        private static IEnumerable<T> GetSource<T>(IEnumerable<T> outer)
        {
            var enumerable = outer as Enumerable<T>;
            return enumerable != null ? enumerable.Source : outer;
        }

        public static IEnumerable<IGrouping<T, T>> Group<T>(this IEnumerable<T> source)
        {
            return source.GroupBy(x => x);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.Join``4(System.Collections.Generic.IEnumerable{``0},VariableIrony.Linq.IOuterEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        public static IEnumerable<TResult> Join<TInner, TOuter, TKey, TResult>(this IEnumerable<TInner> inner, IOuterEnumerable<TOuter> outer, Func<TInner, TKey> innerKeySelector, Func<TOuter, TKey> outerKeySelector, Func<TInner, TOuter, TResult> resultSelector)
        {
            return inner.Join(outer, innerKeySelector, outerKeySelector, resultSelector, null);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.Join``4(System.Collections.Generic.IEnumerable{``0},VariableIrony.Linq.IInnerEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IInnerEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.LeftJoin``4(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        /// <remarks>
        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.LeftJoin``4(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        /// <para>This method is called when using query expressions where the sequence on the right side is an <see cref="T:VariableIrony.Linq.IOuterEnumerable`1" /> (for example, by calling <see cref="M:VariableIrony.IEnumerableExtensions.AsOuter``1(System.Collections.Generic.IEnumerable{``0})" />).</para>
        /// </remarks>
        public static IEnumerable<TResult> Join<TInner, TOuter, TKey, TResult>(this IEnumerable<TInner> inner, IOuterEnumerable<TOuter> outer, Func<TInner, TKey> innerKeySelector, Func<TOuter, TKey> outerKeySelector, Func<TInner, TOuter, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            return LeftJoin(GetSource(outer), inner, outerKeySelector, innerKeySelector, (x, y) => resultSelector(y, x), comparer);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.LeftJoin``4(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        /// <remarks>
        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.LeftJoin``4(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        /// <para>This method is called when using query expressions where the sequence expression on the right side is an <see cref="T:VariableIrony.Linq.IInnerEnumerable`1" /> (for example, by calling <see cref="M:VariableIrony.IEnumerableExtensions.AsInner``1(System.Collections.Generic.IEnumerable{``0})" />).</para>
        /// </remarks>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IInnerEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            return LeftJoin(outer, GetSource(inner), outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.LeftJoin``4(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1},System.Func{``0,``2},System.Func{``1,``2},System.Func{``0,``1,``3},System.Collections.Generic.IEqualityComparer{``2})" />
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return LeftJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        /// <summary>
        /// Correlates all of the elements of an outer sequence with any matching elements of an inner sequence, based on matching keys.
        /// </summary>
        /// <typeparam name="TOuter">The type of the elements of the outer sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the inner sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="outer">The outer sequence to join.</param>
        /// <param name="inner">The inner sequence to join.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the outer sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the inner sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare join keys.</param>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <typeparamref name="TResult" /> that are obtained by performing an outer join on the input sequences.</returns>
        /// <remarks><para>All elements of <paramref name="outer" /> will be selected, even if there is no matching element in <paramref name="inner" />; in that case, the value that is passed as the second parameter to <paramref name="resultSelector" /> will be the default value for <typeparamref name="TInner" />.</para></remarks>
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (o, i) => new { Outer = o, Inner = i }, comparer)
                .SelectMany(x => x.Inner.DefaultIfEmpty(), (x, y) => resultSelector(x.Outer, y));
        }

        public static IEnumerable<TSource> OrderBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> keySelector, Comparison<TResult> comparer)
        {
            return source.OrderBy(keySelector, new Comparer<TResult>(comparer));
        }

        /// <summary>
        /// Prepends an element to the beginning of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">The sequence to which the item should be prepended.</param>
        /// <param name="value">The item to prepend.</param>
        /// <returns>A new sequence that consists of the prepended item, followed by the original sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <seealso cref="M:System.Linq.Enumerable.Concat``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0})" />
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T value)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return Enumerable.Repeat(value, 1).Concat(source);
        }

        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.SequenceEqualUnordered``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEqualityComparer{``0})" />
        /// <summary>
        /// Determines whether two sequences contain the same elements by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <returns><see langword="true" /> if the two source sequences contain the same elements (including the same number of duplicate elements) according to the default equality comparer for their type; otherwise, <see langword="false" />. </returns>
        public static bool SequenceEqualUnordered<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return first.SequenceEqualUnordered(second, null);
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.SequenceEqual``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEqualityComparer{``0})" />
        /// <summary>
        /// Determines whether two sequences contain the same elements by comparing the elements by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
        /// </summary>
        /// <returns><see langword="true" /> if the two source sequences contain the same elements (including the same number of duplicate elements) according to <paramref name="comparer" />; otherwise, <see langword="false" />. </returns>
        /// <remarks></remarks>
        public static bool SequenceEqualUnordered<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            Dictionary<TSource, int> source = first
                .GroupBy(IdentityFunction<TSource>.Instance)
                .ToDictionary(g => g.Key, g => g.Count(), comparer);
            foreach (var local in second)
            {
                int num = source.TryGetValue(local, 0) - 1;
                if (num < 0)
                {
                    return false;
                }
                source[local] = num;
            }
            return source.Values.All(c => c == 0);
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionMessage">The exception message to throw if the sequence is empty or contains more than one element.</param>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, string exceptionMessage)
        {
            TSource local;
            if (!source.TrySingle(out local))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionMessage">The exception message to throw if the sequence is empty or contains more than one element.</param>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string exceptionMessage)
        {
            TSource local;
            if (!source.TrySingle(predicate, out local))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionFormat">The format string of the custom exception message to throw if the sequence is empty or contains more than one element.</param>
        /// <param name="args">The arguments to format.</param>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, string exceptionFormat, params object[] args)
        {
            TSource local;
            if (!source.TrySingle(out local))
            {
                throw new InvalidOperationException(String.Format(exceptionFormat, args));
            }
            return local;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="exceptionFormat">The format string of the custom exception message to throw if the sequence is empty or contains more than one element.</param>
        /// <param name="args">The arguments to format.</param>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string exceptionFormat, params object[] args)
        {
            TSource local;
            if (!source.TrySingle(predicate, out local))
            {
                throw new InvalidOperationException(String.Format(exceptionFormat, args));
            }
            return local;
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.OrderBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})" />
        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(IdentityFunction<T>.Instance);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare elements.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>If <paramref name="comparer" /> is null, the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> is used to compare values.</p>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.OrderBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})" />
        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            return source.OrderBy(IdentityFunction<T>.Instance, comparer);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <returns>A new sequence containing the elements of the input sequence in reverse sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.OrderByDescending``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})" />
        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source)
        {
            return source.OrderByDescending(IdentityFunction<T>.Instance);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare elements.</param>
        /// <returns>A new sequence containing the elements of the input sequence in reverse order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>If <paramref name="comparer" /> is null, the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> is used to compare values.</p>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.OrderByDescending``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})" />
        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            return source.OrderByDescending(IdentityFunction<T>.Instance, comparer);
        }

        /// <summary>
        /// Performs a subsequent sort of the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.ThenBy``2(System.Linq.IOrderedEnumerable{``0},System.Func{``0,``1})" />
        public static IOrderedEnumerable<T> ThenSort<T>(this IOrderedEnumerable<T> source)
        {
            return source.ThenBy(IdentityFunction<T>.Instance);
        }

        /// <summary>
        /// Performs a subsequent sort of the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare elements.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>If <paramref name="comparer" /> is null, the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> is used to compare values.</p>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.ThenBy``2(System.Linq.IOrderedEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})" />
        public static IOrderedEnumerable<T> ThenSort<T>(this IOrderedEnumerable<T> source, IComparer<T> comparer)
        {
            return source.ThenBy(IdentityFunction<T>.Instance, comparer);
        }

        /// <summary>
        /// Performs a subsequent sort of the elements of a sequence in descending order.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.ThenByDescending``2(System.Linq.IOrderedEnumerable{``0},System.Func{``0,``1})" />
        public static IOrderedEnumerable<T> ThenSortDescending<T>(this IOrderedEnumerable<T> source)
        {
            return source.ThenByDescending(IdentityFunction<T>.Instance);
        }

        /// <summary>
        /// Performs a subsequent sort of the elements of a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A sequence of values to sort.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare elements.</param>
        /// <returns>A new sequence containing the elements of the input sequence in sorted order.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />. </exception>
        /// <remarks>
        /// <p>If <paramref name="comparer" /> is null, the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> is used to compare values.</p>
        /// <p>This method performs a stable sort; that is, if two elements are equal, the order of the elements is preserved.</p>
        /// </remarks>
        /// <seealso cref="M:System.Linq.Enumerable.ThenByDescending``2(System.Linq.IOrderedEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})" />
        public static IOrderedEnumerable<T> ThenSortDescending<T>(this IOrderedEnumerable<T> source, IComparer<T> comparer)
        {
            return source.ThenByDescending(IdentityFunction<T>.Instance, comparer);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.HashSet`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.
        /// </summary>
        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.ToSet``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEqualityComparer{``0})" />
        public static HashSet<T> ToSet<T>(this IEnumerable<T> source)
        {
            return source.ToSet(IdentityFunction<T>.Instance, null);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.HashSet`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> using a specified value comparer.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <param name="source" /> and in the resulting <see cref="T:System.Collections.Generic.HashSet`1" />.</typeparam>
        /// <param name="comparer"><inheritdoc cref="M:VariableIrony.IEnumerableExtensions.ToSet``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1})" /></param>
        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.ToSet``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1})" />
        /// <returns>A <see cref="T:System.Collections.Generic.HashSet`1" /> that contains the elements from <paramref name="source" />.</returns>
        public static HashSet<T> ToSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            return source.ToSet(IdentityFunction<T>.Instance, comparer);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.HashSet`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> using a specified selector function.
        /// </summary>
        /// <inheritdoc cref="M:VariableIrony.IEnumerableExtensions.ToSet``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1})" />
        public static HashSet<TElement> ToSet<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> elementSelector)
        {
            return source.ToSet(elementSelector, null);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.HashSet`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> using a specified selector function and value comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" /> and of the elements in the resulting <see cref="T:System.Collections.Generic.HashSet`1" />.</typeparam>
        /// <param name="source">The source of the elements that will fill the <see cref="T:System.Collections.Generic.HashSet`1" />.</param>
        /// <param name="elementSelector">A transform function to produce a result value from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values in the resulting <see cref="T:System.Collections.Generic.HashSet`1" />.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.HashSet`1" /> that contains the transformed elements from <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="elementSelector" /> is <see langword="null" />.</exception>
        /// <remarks>If <paramref name="comparer" /> is <see langword="null" />, the default equality comprarer <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" /> is used to compare keys.</remarks>
        public static HashSet<TElement> ToSet<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> elementSelector, IEqualityComparer<TElement> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (elementSelector == null)
            {
                throw new ArgumentNullException("elementSelector");
            }
            var set = new HashSet<TElement>(comparer);
            foreach (var local in source)
            {
                set.Add(elementSelector(local));
            }
            return set;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="result"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" select="returns" /></param>
        /// <returns><see langword="false" /> if the source sequence is empty; otherwise, <see langword="true" />.</returns>
        public static bool TryFirst<TSource>(this IEnumerable<TSource> source, out TSource result)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    result = list[0];
                    return true;
                }
            }
            else 
            {
                foreach(var local in source) 
                {
                    result = local;
                    return true;
                }
            }
            result = default(TSource);
            return false;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="result"><inheritdoc cref="M:System.Linq.Enumerable.First``1(System.Collections.Generic.IEnumerable{``0})" select="returns" /></param>
        /// <returns><see langword="false" /> if the source sequence is empty; otherwise, <see langword="true" />.</returns>
        public static bool TryFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out TSource result)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            foreach(var local in source) 
            {
                if(predicate(local)) 
                {
                    result = local;
                    return true;                    
                }
            }
            result = default(TSource);
            return false;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="result"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" select="returns" /></param>
        /// <returns><see langword="false" /> if the source sequence is empty or contains more than one element; otherwise, <see langword="true" />.</returns>
        public static bool TrySingle<TSource>(this IEnumerable<TSource> source, out TSource result)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count == 1)
                {
                    result = list[0];
                    return true;
                }
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        if (!enumerator.MoveNext())
                        {
                            result = current;
                            return true;
                        }
                    }
                }
            }
            result = default(TSource);
            return false;
        }

        /// <inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" />
        /// <param name="source"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="predicate"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" /></param>
        /// <param name="result"><inheritdoc cref="M:System.Linq.Enumerable.Single``1(System.Collections.Generic.IEnumerable{``0})" select="returns" /></param>
        /// <returns><see langword="false" /> if the source sequence is empty or contains more than one element; otherwise, <see langword="true" />.</returns>
        public static bool TrySingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out TSource result)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            result = default(TSource);
            bool flag = false;
            foreach(var local in source.Where(predicate)) 
            {
                if (flag)
                {
                    result = default(TSource);
                    return false;
                }
                result = local;
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Returns a sequence obtained by joining two sequences of elements by position.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the resulting sequence.</typeparam>
        /// <param name="first">The first sequence to join.</param>
        /// <param name="second">The sequence to join to the first sequence.</param>
        /// <param name="func">A function which joins together the paired items from each sequence.</param>
        /// <returns>A sequence that has elements of type <typeparamref name="TResult" /> that are obtained by joining together corresponding elements from each sequence.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="first" />, <paramref name="second" />, or <paramref name="func" /> is <see langword="null" />.</exception>
        /// <remarks>
        /// <p>Elements from each sequence are paired until no more elements are found in one or both sequences. If one sequence has more elements than the other, the extra items are not paired.</p>
        /// <p>http://community.bartdesmet.net/blogs/bart/archive/2008/11/03/c-4-0-feature-focus-part-3-intermezzo-linq-s-new-zip-operator.aspx</p>
        /// </remarks>
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> func)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            return first.ZipInternal(second, func);
        }

        private static IEnumerable<TResult> ZipInternal<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> func)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
        
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
			if (null == source) {
				throw new ArgumentNullException("source");
			}
			if (0 >= pageSize) {
				throw new ArgumentOutOfRangeException("pageSize", "'pageSize' must be greater than 0");
			}
			return PageImpl(source, pageSize);
		}

        private static IEnumerable<IEnumerable<T>> PageImpl<T>(this IEnumerable<T> source, int pageSize) {
			var page = new List<T>();
			//TODO: fully defer this
			foreach (var item in source) {
				page.Add(item);
				if (page.Count == pageSize) {
					yield return page;
					page = new List<T>();
				}
			}
			if (page.Count > 0)
				yield return page;
		}

        /// <summary>
        /// Returns elements from a sequence until a specified condition is true.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements from the input sequence that occur before the first element at which the test passes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
		public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
			return source.TakeWhile(x => !predicate(x));
		}

        /// <summary>
        /// Computes the sum of a sequence of <see cref="UInt16"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="UInt16"/> values to calculate the sum of.</param>
        /// <returns>
        /// The sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The sum is larger than <see cref="UInt16.MaxValue"/>.</exception>
        public static ushort Sum(this IEnumerable<ushort> source)
        {
            return source.Aggregate((ushort)0, (current, value) => (ushort) (current + value));
        }

        /// <summary>
        /// Computes the sum of the sequence of <see cref="UInt16"/> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The sum of the projected values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="UInt16.MaxValue"/>.</exception>
        public static ushort Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort> selector)
        {
            return source.Select(selector).Sum();
        }
	}
}
