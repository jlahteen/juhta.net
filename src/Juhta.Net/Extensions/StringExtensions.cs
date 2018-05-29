
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="String"/> class.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks whether this string contains white space characters.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns true if this string contains white space characters, otherwise false.</returns>
        public static bool ContainsWhitespaces(this string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (Char.IsWhiteSpace(s, i))
                    return(true);

            return(false);
        }

        /// <summary>
        /// Ensures this string to end with a specified string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="end">Specifies a string with which this string is ensured to end with.</param>
        /// <returns>Returns such copy of this string that ends with <paramref name="end"/>.</returns>
        public static string EnsureEnd(this string s, string end)
        {
            return(s.EnsureEnd(end, StringComparison.Ordinal));
        }

        /// <summary>
        /// Ensures this string to end with a specified string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="end">Specifies a string with which this string is ensured to end with.</param>
        /// <param name="stringComparison">Specifies a StringComparison value.</param>
        /// <returns>Returns such copy of this string that ends with <paramref name="end"/>.</returns>
        public static string EnsureEnd(this string s, string end, StringComparison stringComparison)
        {
            int i;

            if (s.EndsWith(end, stringComparison))
                return(s);

            else
                for (i = end.Length - 1; i > 0; i--)
                    if ((s + end.Substring(i)).EndsWith(end, stringComparison))
                        return(s + end.Substring(i));

            return(s + end);
        }

        /// <summary>
        /// Converts this base64 string to its equivalent string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns the string representation of this base64 string.</returns>
        /// <remarks>This method performs string conversions through the UTF-8 encoding.</remarks>
        public static string FromBase64String(this string s)
        {
            byte[] bytes = Convert.FromBase64String(s);

            return(UTF8Encoding.UTF8.GetString(bytes));
        }

        /// <summary>
        /// Checks whether a specified regular expression pattern matches this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="pattern">Specifies a regular expression pattern.</param>
        /// <returns>Returns true if the specified regular expression pattern matches this string, otherwise false.</returns>
        public static bool IsRegexMatch(this string s, string pattern)
        {
            return(Regex.IsMatch(s, pattern));
        }

        /// <summary>
        /// Removes a specified end from this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="end">Specifies a string that will be removed from the end of this string.</param>
        /// <returns>Returns such copy of this string from which the specified end has been removed. If this string
        /// doesn't end with the specified end, returns a copy of this string.</returns>
        /// <remarks>This method is case-sensitive.</remarks>
        public static string RemoveEnd(this string s, string end)
        {
            return(RemoveEnd(s, end, StringComparison.Ordinal));
        }

        /// <summary>
        /// Removes a specified end from this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="end">Specifies a string that will be removed from the end of this string.</param>
        /// <param name="stringComparison">Specifies a StringComparison value.</param>
        /// <returns>Returns such copy of this string from which the specified end has been removed. If this string
        /// doesn't end with the specified end, returns a copy of this string.</returns>
        public static string RemoveEnd(this string s, string end, StringComparison stringComparison)
        {
            if (s.EndsWith(end, stringComparison))
                return(s.Substring(0, s.Length - end.Length));
            else
                return(s);
        }

        /// <summary>
        /// Removes a specified start from this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="start">Specifies a string that will be removed from the start of this string.</param>
        /// <returns>Returns such copy of this string from which the specified start has been removed. If this string
        /// doesn't start with the specified start, returns a copy of this string.</returns>
        /// <remarks>This method is case-sensitive.</remarks>
        public static string RemoveStart(this string s, string start)
        {
            return(RemoveStart(s, start, StringComparison.Ordinal));
        }

        /// <summary>
        /// Removes a specified start from this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="start">Specifies a string that will be removed from the start of this string.</param>
        /// <param name="stringComparison">Specifies a StringComparison value.</param>
        /// <returns>Returns such copy of this string from which the specified start has been removed. If this string
        /// doesn't start with the specified start, returns a copy of this string.</returns>
        public static string RemoveStart(this string s, string start, StringComparison stringComparison)
        {
            if (s.StartsWith(start, stringComparison))
                return(s.Substring(start.Length));
            else
                return(s);
        }

        /// <summary>
        /// Replaces all HTML special characters with their corresponding entities in this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns such copy of this string where all occurrences of HTML special characters have been
        /// replaced with their corresponding entities.</returns>
        public static string ReplaceHtmlSpecialCharacters(this string s)
        {
            if (s == null)
                return(s);

            s = s.Replace("&", "&amp;");

            s = s.Replace("<", "&lt;");

            s = s.Replace(">", "&gt;");

            s = s.Replace("\"", "&quot;");

            s = s.Replace("'", "&#39;");  // &apos; doesn't work in IE

            return(s);
        }

        /// <summary>
        /// Replaces all XML special characters with their corresponding entities in this string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns such copy of this string where all occurrences of XML special characters have been
        /// replaced with their corresponding entities.</returns>
        public static string ReplaceXmlSpecialCharacters(this string s)
        {
            if (s == null)
                return(s);

            s = s.Replace("&", "&amp;");

            s = s.Replace("<", "&lt;");

            s = s.Replace(">", "&gt;");

            s = s.Replace("\"", "&quot;");

            s = s.Replace("'", "&apos;");

            return(s);
        }

        /// <summary>
        /// Gets a substring from this string after the first occurrence of a specified character.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="value">Specifies a character to find.</param>
        /// <param name="comparison">Specifies a comparison mode.</param>
        /// <returns>If <paramref name="value"/> is found within the current string, returns the substring from the
        /// current string after the first occurrence of <paramref name="value"/>. If <paramref name="value"/> is not
        /// found, returns the current string; this will be done also when <paramref name="value"/> is null or empty.</returns>
        public static string SubstringAfter(this string s, char value, StringComparison comparison)
        {
            return(s.SubstringAfter(value.ToString(), comparison));
        }

        /// <summary>
        /// Gets a substring from this string after the first occurrence of a specified string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="value">Specifies a string to find.</param>
        /// <param name="comparison">Specifies a comparison mode.</param>
        /// <returns>If <paramref name="value"/> is found within the current string, returns the substring from the
        /// current string after the first occurrence of <paramref name="value"/>. If <paramref name="value"/> is not
        /// found, returns the current string; this will be done also when <paramref name="value"/> is null or empty.</returns>
        public static string SubstringAfter(this string s, string value, StringComparison comparison)
        {
            int position;

            if (String.IsNullOrEmpty(value))
                return(s);

            position = s.IndexOf(value, comparison);

            if (position >= 0)
                return(s.Substring(position + value.Length));
            else
                return(s);
        }

        /// <summary>
        /// Gets a substring from this string before the first occurrence of a specified character.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="value">Specifies a character to find.</param>
        /// <param name="comparison">Specifies a comparison mode.</param>
        /// <returns>If <paramref name="value"/> is found within the current string, returns the substring from the
        /// current string before the first occurrence of <paramref name="value"/>. If <paramref name="value"/> is not
        /// found, returns the current string; this will be done also when <paramref name="value"/> is null or empty.</returns>
        public static string SubstringBefore(this string s, char value, StringComparison comparison)
        {
            return(s.SubstringBefore(value.ToString(), comparison));
        }

        /// <summary>
        /// Gets a substring from this string before the first occurrence of a specified string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <param name="value">Specifies a string to find.</param>
        /// <param name="comparison">Specifies a comparison mode.</param>
        /// <returns>If <paramref name="value"/> is found within the current string, returns the substring from the
        /// current string before the first occurrence of <paramref name="value"/>. If <paramref name="value"/> is not
        /// found, returns the current string; this will be done also when <paramref name="value"/> is null or empty.</returns>
        public static string SubstringBefore(this string s, string value, StringComparison comparison)
        {
            int position;

            if (String.IsNullOrEmpty(value))
                return(s);

            position = s.IndexOf(value, comparison);

            if (position >= 0)
                return(s.Substring(0, position));
            else
                return(s);
        }

        /// <summary>
        /// Converts this string to its equivalent base64 string.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns the base64 string representation of this string.</returns>
        /// <remarks>This method performs base64 string conversions through the UTF-8 encoding.</remarks>
        public static string ToBase64String(this string s)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(s);

            return(Convert.ToBase64String(bytes));
        }

        /// <summary>
        /// Converts this string to an equivalent <see cref="System.Boolean"/> value.
        /// </summary>
        /// <param name="s">Specifies the current string instance.</param>
        /// <returns>Returns this string as an equivalent <see cref="System.Boolean"/> value.</returns>
        /// <remarks>This method is otherwise equivalent to <see cref="Convert.ToBoolean(string)"/>, but it also
        /// accepts the values "1" and "0" as valid string representatives for the boolean values True and False,
        /// respectively.</remarks>
        public static bool ToBoolean(this string s)
        {
            if (s == "0")
                return(false);

            else if (s == "1")
                return(true);

            else
                return(Convert.ToBoolean(s));
        }

        #endregion
    }
}
