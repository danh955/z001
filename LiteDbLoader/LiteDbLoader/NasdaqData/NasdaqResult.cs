// <copyright file="NasdaqResult.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader.NasdaqData
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NASDAQ result class.
    /// </summary>
    /// <typeparam name="T">The type of each item element.</typeparam>
    public class NasdaqResult<T>
        where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NasdaqResult{T}"/> class.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <param name="fileCreationTime">fFile creation time.</param>
        public NasdaqResult(IEnumerable<T> items, DateTime? fileCreationTime)
        {
            this.Items = items;
            this.FileCreationTime = fileCreationTime;
        }

        /// <summary>
        /// Gets or sets file creation time.
        /// </summary>
        public DateTime? FileCreationTime { get; set; }

        /// <summary>
        /// Gets or sets data items.
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}