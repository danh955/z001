// <copyright file="LiteDbStockRepository.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Storage.LiteDbRepository
{
    using System.Data;
    using System.Threading.Tasks;
    using DDD1.Core.Domain.StockAggregate;
    using DDD1.Core.Storage;
    using LiteDB;

    // Note on LiteDB: The next version 5.5 will have a-sync/await implemented.
    // https://github.com/mbdavid/LiteDB/issues/1300

    /// <summary>
    /// Stock repository class.
    /// </summary>
    internal class LiteDbStockRepository : IStockRepository
    {
        private readonly ILiteCollection<Stock> stocks;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbStockRepository"/> class.
        /// </summary>
        /// <param name="db">Lite database.</param>
        public LiteDbStockRepository(LiteDatabase db)
        {
            if (db == null)
            {
                throw new NoNullAllowedException();
            }

            this.stocks = db.GetCollection<Stock>("Stock");
            this.stocks.EnsureIndex(s => s.Symbol, unique: true);
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="entity">Stock to add.</param>
        /// <returns>ID of new entity.</returns>
        public async Task<int> AddAsync(Stock entity)
        {
            return await Task<int>.FromResult(this.stocks.Insert(entity)).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">ID of stock to delete.</param>
        /// <returns>True if deleted.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await Task<bool>.FromResult(this.stocks.Delete(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Find by ID.
        /// </summary>
        /// <param name="id">ID of the stock to find.</param>
        /// <returns>Stock.  Null if not found.</returns>
        public async Task<Stock> FindByIdAsync(int id)
        {
            return await Task.FromResult(this.stocks.FindById(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Find by name.
        /// </summary>
        /// <param name="symbol">Symbol of the stock to find.</param>
        /// <returns>Stock.  Null if not found.</returns>
        public async Task<Stock> FindBySymbolAsync(string symbol)
        {
            return await Task.FromResult(this.stocks.FindOne(s => s.Symbol == symbol)).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if symbol exists.
        /// </summary>
        /// <param name="symbol">Symbol of the stock to find.</param>
        /// <returns>True if found.</returns>
        public async Task<bool> ExistsBySymbolAsync(string symbol)
        {
            return await Task.FromResult(this.stocks.Exists(s => s.Symbol == symbol)).ConfigureAwait(false);
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity">Stock to update.</param>
        /// <returns>True if updated.</returns>
        public async Task<bool> UpdateAsync(Stock entity)
        {
            return await Task<bool>.FromResult(this.stocks.Update(entity)).ConfigureAwait(false);
        }
    }
}