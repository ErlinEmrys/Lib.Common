using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Erlin.Lib.Common.Exceptions;

namespace System.Linq
{
    /// <summary>
    /// Helper class for common extension methods for system types
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Withdraw all items from list by provided selector and returns them as separate list
        /// </summary>
        /// <typeparam name="TSource">Runtime type of list item</typeparam>
        /// <param name="source">Source list</param>
        /// <param name="selector">Item selector</param>
        /// <returns>Withdrawed items as list</returns>
        public static List<TSource> Withdraw<TSource>(this IList<TSource> source, Func<TSource, bool> selector)
        {
            List<TSource> result = new List<TSource>();
            for (int i = 0; i < source.Count;)
            {
                TSource item = source[i];
                if (selector(item))
                {
                    result.Add(item);
                    source.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            return result;
        }

        /// <summary>
        /// Easy convert enumerables
        /// </summary>
        /// <typeparam name="TSource">Source runtime type</typeparam>
        /// <typeparam name="TResult">Target runtime type</typeparam>
        /// <param name="source">Collection of source objects</param>
        /// <param name="converter">Converter function</param>
        /// <returns>Collection of target objects</returns>
        public static IEnumerable<TResult> Convert<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> converter)
        {
            switch (source)
            {
                case IEnumerable<TResult> results:
                    return results;
                default:
                    return source.Select(converter);
            }
        }

        /// <summary>
        /// Select all items from collection with min value specified
        /// </summary>
        /// <typeparam name="TSource">Runtime type of items</typeparam>
        /// <typeparam name="TKey">Runtime type of comparable values</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="selector">Min value selector</param>
        /// <returns>Collection with min values</returns>
        public static IEnumerable<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
            where TKey : IComparable
        {
            TKey minValue = source.Min(selector);
            return source.Where(i => selector.Invoke(i).CompareTo(minValue) == 0);
        }

        /// <summary>
        /// Select all items from collection with max value specified
        /// </summary>
        /// <typeparam name="TSource">Runtime type of items</typeparam>
        /// <typeparam name="TKey">Runtime type of comparable values</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="selector">Max value selector</param>
        /// <returns>Collection with max values</returns>
        public static IEnumerable<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
            where TKey : IComparable
        {
            if (source.Any())
            {
                TKey maxValue = source.Max(selector);
                return source.Where(i => selector.Invoke(i).CompareTo(maxValue) == 0);
            }

            return Enumerable.Empty<TSource>();
        }

        /// <summary>
        /// Clears concurrent queue
        /// </summary>
        /// <typeparam name="T">Runtime type of stored objects</typeparam>
        /// <param name="queue">Queue to clear</param>
        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            while (queue.Count > 0)
            {
                queue.TryDequeue(out T _);
            }
        }
    }
}

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Helper class for common extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        internal const string STACKTRACE_PRESERVE = "CatchStackTrace";
        internal const string STACKTRACE_TASK = "StartThreadStackTrace";

#region Type

        /// <summary>
        /// Returns one custom attribute from runtime type
        /// </summary>
        /// <typeparam name="T">Runtime type of attribute to retrieve</typeparam>
        /// <param name="type">Runtime type</param>
        /// <param name="inherit">True - return inherited attribute</param>
        /// <returns>Custom attribute object or NULL</returns>
        public static T? GetOneCustomAttribute<T>(this Type type, bool inherit = true)
            where T : Attribute
        {
            Type attributeType = typeof(T);
            object[] attributes;
            try
            {
                attributes = type.GetCustomAttributes(attributeType, inherit);
            }
            catch (AttributeCreationException ex)
            {
                throw new InvalidProgramException($"GetOneCustomAttribute - could not get custom attributes on object type: {type} because of attribute ctor error: {ex.Message}");
            }

            if (attributes.Length > 1)
            {
                throw new InvalidProgramException($"GetOneCustomAttribute method foud more than one attribute: {attributeType} on object: {type.FullName}");
            }

            if (attributes.Length == 0)
            {
                return null;
            }

            return attributes[0] as T;
        }

#endregion

#region Enums

        /// <summary>
        /// Returns associatted name on enum field
        /// </summary>
        /// <param name="value">Enum field value</param>
        /// <returns>Associatted name</returns>
        public static string Name(this Enum value)
        {
            // get attributes
            FieldInfo? field = value.GetType().GetTypeInfo().GetField(value.ToString());
            if (field == null)
            {
                throw new InvalidOperationException("Field not found by refactoring!");
            }

            IEnumerable<NameDescriptionAttribute> attributes = field.GetCustomAttributes<NameDescriptionAttribute>(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            NameDescriptionAttribute displayAttribute = attributes.SingleOrDefault();

            // return description
            return displayAttribute?.Name ?? $"NameDescriptionAttribute Not Found on field {value}";
        }

#endregion

#region Exception

        private static MethodInfo? PreserveStackTraceMethod { get; } = typeof(Exception).GetMethod("InternalPreserveStackTrace",
                                                                                                   BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Preserve stacktrace in exception object
        /// </summary>
        /// <param name="exception">Exception to preserve stacktrace</param>
        public static void PreserveStackTrace(this Exception exception)
        {
            PreserveStackTraceMethod?.Invoke(exception, null);
            exception.Data.Add(STACKTRACE_PRESERVE, EnvironmentHelper.GetStackTrace());
        }

        /// <summary>
        /// Returns full descriptive text of any exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Text representation of exception</returns>
        public static string ToStringFull(this Exception exception)
        {
            string text = exception.ToString();
            if (exception.Data.Contains(STACKTRACE_PRESERVE))
            {
                string additionalStack = (string)exception.Data[STACKTRACE_PRESERVE]!;
                text += "---> CATCH <---" + Environment.NewLine;
                text += additionalStack;
            }

            if (exception.Data.Contains(STACKTRACE_TASK))
            {
                string additionalStack = (string)exception.Data[STACKTRACE_TASK]!;
                text += "---> Task.Run <---" + Environment.NewLine;
                text += additionalStack;
            }

            return text;
        }

#endregion

#region String

        /// <summary>
        /// Covert entered string to integer hash code
        /// </summary>
        /// <param name="value">Text</param>
        /// <param name="toLower">Use lowercase before converting</param>
        /// <returns>Hash code</returns>
        public static unsafe int ToHashCode(this string? value, bool toLower = true)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            value = toLower ? value.ToLowerInvariant() : value;
            fixed (char* str = value)
            {
                int num = 0x15051505;
                int num2 = num;
                int* numPtr = (int*)str;
                for (int i = value.Length; i > 0; i -= 4)
                {
                    num = ((num << 5) + num + (num >> 0x1b)) ^ numPtr[0];
                    if (i <= 2)
                    {
                        break;
                    }

                    num2 = ((num2 << 5) + num2 + (num2 >> 0x1b)) ^ numPtr[1];
                    numPtr += 2;
                }

                return num + num2 * 0x5d588b65;
            }
        }

        /// <summary>
        /// Makes comparison of two strings (ignoring case)
        /// </summary>
        /// <param name="text">First text</param>
        /// <param name="compareTo">Second text</param>
        /// <returns>True - strings are equal</returns>
        public static bool CompareIgnoreCase(this string? text, string? compareTo)
        {
            return string.Equals(text, compareTo, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Remove diacritics from text
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Text without diacritics</returns>
        public static string? RemoveDiacritics(this string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Check if string contains basic chars = A-z1-9
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>True - string contains basic chars</returns>
        public static bool ContainsBasicChars(this string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            foreach (char fChar in text)
            {
                if (char.IsLetterOrDigit(fChar))
                {
                    return true;
                }
            }

            return false;
        }

#endregion

#region Collections

        /// <summary>
        /// Separate collections items to three ways (Only items in left collection, only items in right collection, items in both collections)
        /// </summary>
        /// <typeparam name="T">Runtime type of item</typeparam>
        /// <param name="left">Left collection of items to separate</param>
        /// <param name="right">Right collection of items to separate</param>
        /// <param name="equalityMethod">Method that check equality of items</param>
        /// <param name="onlyLeft">Method for perform items found only in left collection</param>
        /// <param name="onlyRight">Method for perform items found only in right collection</param>
        /// <param name="leftRight">Method for perform items found in both collections</param>
        public static void Indiferente<T>(this IEnumerable<T>? left, IEnumerable<T>? right, Func<T, T, bool> equalityMethod,
                                          Action<T>? onlyLeft = null, Action<T>? onlyRight = null, Action<T, T>? leftRight = null)
        {
            if (left == null)
            {
                left = new List<T>();
            }

            if (right == null)
            {
                right = new List<T>();
            }

            List<T> tempRight = right.ToList();
            foreach (T fLeft in left)
            {
                T fRight = tempRight.FirstOrDefault(t => equalityMethod(fLeft, t));
                if (!ReferenceEquals(fRight, null))
                {
                    leftRight?.Invoke(fLeft, fRight);
                    tempRight.Remove(fRight);
                }
                else
                {
                    onlyLeft?.Invoke(fLeft);
                }
            }

            if (onlyRight != null)
            {
                foreach (T fRight in tempRight)
                {
                    onlyRight(fRight);
                }
            }
        }

        /// <summary>
        /// Remove range of items
        /// </summary>
        /// <typeparam name="T">Runtime of item type</typeparam>
        /// <param name="source">Collection to remove items</param>
        /// <param name="toRemove">Which items should be removed</param>
        public static void RemoveRange<T>(this IList<T> source, IEnumerable<T>? toRemove)
        {
            if (toRemove != null)
            {
                foreach (T fItem in toRemove)
                {
                    source.Remove(fItem);
                }
            }
        }

#endregion
    }
}