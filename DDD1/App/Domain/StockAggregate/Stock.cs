// <copyright file="Stock.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Domain.StockAggregate
{
    /// <summary>
    /// Stock class.
    /// </summary>
    public class Stock : IEntity
    {
        /// <summary>
        /// Gets or sets iD.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }
    }
}