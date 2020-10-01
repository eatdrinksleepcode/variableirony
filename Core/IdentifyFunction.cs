using System;

namespace VariableIrony
{
    /// <summary>
    /// Represents a function which returns its input.
    /// </summary>
    public static class IdentityFunction<TElement>
    {
        /// <summary>
        /// Returns a cached instance of the identity function for the specified type parameter.
        /// </summary>
        /// <value>An instance of the identity function.</value>
        public static Func<TElement, TElement> Instance => x => x;
    }
}
