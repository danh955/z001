// <copyright file="StockRepositoryTest.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Test.Core.Storage.LiteDb
{
    using System.Threading.Tasks;
    using DDD1.Core.Domain.StockAggregate;
    using DDD1.Core.Storage;
    using DDD1.Storage.LiteDbRepository;
    using Xunit;

    /// <summary>
    /// Stock repository test class.
    /// </summary>
    public class StockRepositoryTest
    {
        private const int TestId1 = 1;
        private const string TestSymbol1 = "test #1";
        private const string TestNname1 = "Testing #1";

        /// <summary>
        /// Open and close database test.
        /// </summary>
        [Fact]
        public void OpenCloseDatabaseTest()
        {
            using var db = GetLiteDbRepository();

            Assert.NotNull(db);
            Assert.NotNull(db.Stocks);
            Assert.IsAssignableFrom<IStockRepository>(db.Stocks);
        }

        /// <summary>
        /// Add a stock test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AddAsyncTest()
        {
            using var db = await GetLiteDbRepositoryWithDataAsync().ConfigureAwait(false);

            var item = await db.Stocks.FindByIdAsync(1).ConfigureAwait(false);

            Assert.NotNull(item);
            Assert.Equal(TestSymbol1, item.Symbol);
            Assert.Equal(TestNname1, item.Name);
        }

        /// <summary>
        /// Delete a stock test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteAsyncTest()
        {
            using var db = await GetLiteDbRepositoryWithDataAsync().ConfigureAwait(false);

            bool deleted = await db.Stocks.DeleteAsync(1).ConfigureAwait(false);

            Assert.True(deleted);

            deleted = await db.Stocks.DeleteAsync(1).ConfigureAwait(false);

            Assert.False(deleted);
        }

        /// <summary>
        /// Update a stock test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateAsync()
        {
            const string newName = "Updated";

            using var db = await GetLiteDbRepositoryWithDataAsync().ConfigureAwait(false);

            var item = new Stock { Id = 1, Symbol = TestSymbol1, Name = newName };
            bool updated = await db.Stocks.UpdateAsync(item).ConfigureAwait(false);

            Assert.True(updated);

            var item1 = await db.Stocks.FindByIdAsync(1).ConfigureAwait(false);

            Assert.Equal(newName, item1.Name);

            item.Id = 2;
            bool maybeUpdated = await db.Stocks.UpdateAsync(item).ConfigureAwait(false);

            Assert.False(maybeUpdated);
        }

        /// <summary>
        /// Find stock by ID Test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task FindByIdAsyncTest()
        {
            using var db = await GetLiteDbRepositoryWithDataAsync().ConfigureAwait(false);

            var foundItem = await db.Stocks.FindByIdAsync(1).ConfigureAwait(false);

            Assert.NotNull(foundItem);
            Assert.Equal(TestSymbol1, foundItem.Symbol);

            var notfoundItem = await db.Stocks.FindByIdAsync(2).ConfigureAwait(false);

            Assert.Null(notfoundItem);
        }

        /// <summary>
        /// Get lite database repository.
        /// </summary>
        /// <returns>LiteDbRepository.</returns>
        private static LiteDbRepository GetLiteDbRepository()
        {
            return new LiteDbRepository("Filename=:memory:;Connection=Direct");
        }

        /// <summary>
        /// Get lite database repository with test data.
        /// </summary>
        /// <returns>LiteDbRepository.</returns>
        private static async Task<LiteDbRepository> GetLiteDbRepositoryWithDataAsync()
        {
            var db = GetLiteDbRepository();
            int newId = await db.Stocks.AddAsync(new Stock { Symbol = TestSymbol1, Name = TestNname1 }).ConfigureAwait(false);

            Assert.Equal(TestId1, newId);
            return db;
        }
    }
}