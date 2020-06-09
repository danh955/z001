// <copyright file="LiteDbRepository.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Storage.LiteDbRepository
{
    using System;
    using DDD1.Core.Storage;
    using LiteDB;

    /// <summary>
    /// LiteDB database repository class.
    /// </summary>
    public class LiteDbRepository : IDisposable
    {
        private LiteDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbRepository"/> class.
        /// Connection string "Filename={dataFilePath};Connection=direct"
        /// </summary>
        /// <param name="connectionString">LiteDB connection string.</param>
        public LiteDbRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.db = new LiteDatabase(connectionString);
            this.Stocks = new LiteDbStockRepository(this.db);
        }

        /// <summary>
        /// Gets stock collection.
        /// </summary>
        public IStockRepository Stocks { get; private set; }

        /// <summary>
        /// Implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalize and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">True if to despise and close the database.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (this.db != null)
            {
                // If disposing is true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.db.Dispose();
                }

                // Disposing has been done.
                this.db = null;
                this.Stocks = null;
            }
        }
    }
}