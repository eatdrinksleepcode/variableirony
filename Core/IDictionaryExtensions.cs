using System;
using System.Collections.Generic;

namespace VariableIrony {
/// <summary>
/// Provides extension methods for <see cref="T:System.Collections.Generic.IDictionary`2" />.
/// </summary>
public static class IDictionaryExtensions
{
    /// <summary>
    /// Adds a key/value pair to the dictionary if the key does not already exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source"></param>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="valueFactory">The function used to generate a value for the key.</param>
    /// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" />, <paramref name="key" />, or <paramref name="valueFactory" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> valueFactory)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        if (!source.TryGetValue(key, out var local))
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }
            local = valueFactory(key);
            source.Add(key, local);
        }
        return local;
    }

    /// <summary>
    /// Adds a key/value pair to the dictionary if the key does not already exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source"></param>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value to be added, if the key does not already exist.</param>
    /// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary,
    ///  or the new value for the key as returned by valueFactory if the key was not in the dictionary.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" /> or <paramref name="key" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (!source.TryGetValue(key, out var local))
        {
            local = value;
            source.Add(key, local);
        }
        return local;
    }

    /// <summary>
    /// Gets the value associated with the specified key, or a default value if the key is not present.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The types of the values in the dictionary.</typeparam>
    /// <param name="source">The dictionary to retrieve a value from.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the default value for <typeparamref name="TValue" />.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" /> or <paramref name="key" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
    {
        return source.TryGetValue(key, default);
    }

    /// <summary>
    /// Gets the value associated with the specified key, or a default value if the key is not present.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The types of the values in the dictionary.</typeparam>
    /// <param name="source">The dictionary to retrieve a value from.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="defaultValue">The value to return if the <paramref name="key" /> is not found in the dictionary.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the value of <paramref name="defaultValue" />.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" /> or <paramref name="key" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue defaultValue)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof( source));
        }

        return source.TryGetValue(key, out var local) ? local : defaultValue;
    }

    /// <summary>
    /// Gets the value associated with the specified key, or a default value if the key is not present.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The types of the values in the dictionary.</typeparam>
    /// <param name="source">The dictionary to retrieve a value from.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="defaultValue">The value to return if the <paramref name="key" /> is not found in the dictionary.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the value of <paramref name="defaultValue" />.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" /> or <paramref name="key" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return source.TryGetValue(key, out var local) ? local : defaultValue;
    }

    /// <summary>
    /// Gets the value associated with the specified key, or a default value if the key is not present.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The types of the values in the dictionary.</typeparam>
    /// <param name="source">The dictionary to retrieve a value from.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="defaultValue">The value to return if the <paramref name="key" /> is not found in the dictionary.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the value of <paramref name="defaultValue" />.</returns>
    /// <exception cref="ArgumentNullException">
    /// <para><paramref name="source" /> or <paramref name="key" /> is <see langword="null" />.</para>
    /// </exception>
    public static TValue TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return source.TryGetValue(key, out var local) ? local : defaultValue;
    }
}
}
