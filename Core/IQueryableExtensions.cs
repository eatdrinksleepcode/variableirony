using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using VariableIrony.Linq;

namespace VariableIrony
{
    /// <summary>
    /// Provides extension methods for <see cref="T:System.Linq.IQueryable`1" />.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Returns the input typed as <see cref="T:VariableIrony.Linq.IInnerQueryable`1" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The sequence to type as <see cref="T:VariableIrony.Linq.IInnerQueryable`1" />.</param>
        /// <returns>The input sequence typed as <see cref="T:VariableIrony.Linq.IInnerQueryable`1" />.</returns>
        /// <remarks>This method can be called inside a query expression to mark a sequence as an inner side of an outer join.</remarks>
        /// <seealso cref="M:VariableIrony.IQueryableExtensions.Join``4(System.Linq.IQueryable{``0},VariableIrony.Linq.IOuterQueryable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        public static IInnerQueryable<T> AsInner<T>(this IQueryable<T> source)
        {
            return new Queryable<T>(source);
        }

        /// <summary>
        /// Returns the input typed as <see cref="T:VariableIrony.Linq.IOuterQueryable`1" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The sequence to type as <see cref="T:VariableIrony.Linq.IOuterQueryable`1" />.</param>
        /// <returns>The input sequence typed as <see cref="T:VariableIrony.Linq.IOuterQueryable`1" />.</returns>
        /// <remarks>This method can be called inside a query expression to mark a sequence as an outer side of an outer join.</remarks>
        /// <seealso cref="M:VariableIrony.IQueryableExtensions.Join``4(System.Linq.IQueryable{``0},VariableIrony.Linq.IOuterQueryable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        public static IOuterQueryable<T> AsOuter<T>(this IQueryable<T> source)
        {
            return new Queryable<T>(source);
        }

        private static IQueryable<T> GetSource<T>(IQueryable<T> outer)
        {
            var queryable = outer as Queryable<T>;
            return queryable != null ? queryable.Source : outer;
        }

        /// <inheritdoc cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        /// <remarks>
        /// <inheritdoc cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        /// <para>
        /// This method is called when using query expressions where the sequence expression on the right side is an <see cref="T:VariableIrony.Linq.IOuterQueryable`1" /> (for example, by calling <see cref="M:VariableIrony.IQueryableExtensions.AsOuter``1(System.Linq.IQueryable{``0})" />).
        /// If not using query expressions, do not call this method directly; instead, call <see cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />.
        /// </para>
        /// </remarks>
        public static IQueryable<TResult> Join<TInner, TOuter, TKey, TResult>(this IQueryable<TInner> inner, IOuterQueryable<TOuter> outer, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TOuter, TResult>> resultSelector)
        {
            ParameterExpression expression = Expression.Parameter(typeof(TInner), "i");
            ParameterExpression expression2 = Expression.Parameter(typeof(TOuter), "o");
            return GetSource(outer).LeftJoin(inner, outerKeySelector, innerKeySelector, Expression.Lambda<Func<TOuter, TInner, TResult>>(Expression.Invoke(resultSelector, new Expression[] { expression, expression2 }), new[] { expression2, expression }));
        }

        /// <inheritdoc cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        /// <remarks>
        /// <inheritdoc cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />
        /// <para>
        /// This method is called when using query expressions where the sequence expression on the right side is an <see cref="T:VariableIrony.Linq.IInnerQueryable`1" /> (for example, by calling <see cref="M:VariableIrony.IQueryableExtensions.AsInner``1(System.Linq.IQueryable{``0})" />).
        /// If not using query expressions, do not call this method directly; instead, call <see cref="M:VariableIrony.IQueryableExtensions.LeftJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})" />.
        /// </para>
        /// </remarks>
        public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IInnerQueryable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            return outer.LeftJoin(GetSource(inner), outerKeySelector, innerKeySelector, resultSelector);
        }

        private static Expression<Func<TOuter, TInner, TResult>> Lambda<TOuter, TInner, TResult>(TOuter outerExample, TInner innerExample, TResult resultExample, Expression body, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<Func<TOuter, TInner, TResult>>(body, parameters);
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
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <typeparamref name="TResult" /> that are obtained by performing an outer join on the input sequences.</returns>
        /// <remarks><para>All elements of <paramref name="outer" /> will be selected, even if there is no matching element in <paramref name="inner" />; in that case, the value that is passed as the second parameter to <paramref name="resultSelector" /> will be the default value for <typeparamref name="TInner" />.</para></remarks>
        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            if (outer == null)
            {
                throw new ArgumentNullException("outer");
            }
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }
            if (outerKeySelector == null)
            {
                throw new ArgumentNullException("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw new ArgumentNullException("innerKeySelector");
            }
            if (resultSelector == null)
            {
                throw new ArgumentNullException("resultSelector");
            }
            // TODO: this
            var outerExample = new
            {
                o = default(TOuter),
                i = (IEnumerable<TInner>)null
            };
            Type type = outerExample.GetType();
            ParameterExpression expression = Expression.Parameter(type, "a");
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (o, i) => new { o, i })
                .SelectMany(
                    x => x.i.DefaultIfEmpty(), 
                    Lambda(
                        outerExample, 
                        default(TInner), 
                        default(TResult), 
                        ReplaceVisitor.Replace(resultSelector.Body, resultSelector.Parameters[0], Expression.Property(expression, type.GetProperty("o"))), 
                        new[] { expression, resultSelector.Parameters[1] }
                    )
                );
        }
    }
}
