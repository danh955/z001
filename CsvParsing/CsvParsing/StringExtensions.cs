// <copyright file="StringExtensions.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace CsvParsing
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// String extensions class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets an array of string.
        /// </summary>
        /// <param name="text">Text to parse.</param>
        /// <param name="removeEmptyLines">Remove empty lines.</param>
        /// <returns>An array of strings.</returns>
        public static IEnumerable<string> GetLines(this string text, bool removeEmptyLines = false)
        {
            using var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (removeEmptyLines && string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                yield return line;
            }
        }

        /// <summary>
        /// Get a enumerable list of string arrays.
        /// </summary>
        /// <param name="text">The text the CSV is in.</param>
        /// <param name="separator">CSV separator character. Default is a comma (,).</param>
        /// <returns>An array of string arrays.</returns>
        public static IEnumerable<string[]> GetCSVs(this string text, char separator = ',')
        {
            foreach (string line in text.GetLines(true))
            {
                yield return line.Split(separator);
            }
        }
    }
}