// <copyright file="IStockRepository.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Storage
{
    using System.Threading.Tasks;
    using DDD1.Core.Domain.StockAggregate;

    /// <summary>
    /// Stock repository interface.
    /// </summary>
    public interface IStockRepository : IRepository<Stock, int>
    {
        /// <summary>
        /// Find by name.
        /// </summary>
        /// <param name="symbol">Symbol of the stock to find.</param>
        /// <returns>Stock.  Null if not found.</returns>
        Task<Stock> FindBySymbolAsync(string symbol);

        /// <summary>
        /// Check if symbol exists.
        /// </summary>
        /// <param name="symbol">Symbol of the stock to find.</param>
        /// <returns>True if found.</returns>
        Task<bool> ExistsBySymbolAsync(string symbol);
    }
}