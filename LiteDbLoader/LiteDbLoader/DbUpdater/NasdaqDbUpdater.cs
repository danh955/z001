// <copyright file="NasdaqDbUpdater.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader.DbUpdater
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using LiteDbLoader.Model;

    /// <summary>
    /// NASDAQ database updater class.
    /// </summary>
    public class NasdaqDbUpdater
    {
        private readonly IStockRepository stocks;

        /// <summary>
        /// Initializes a new instance of the <see cref="NasdaqDbUpdater"/> class.
        /// </summary>
        /// <param name="stocks">IStockRepository.</param>
        public NasdaqDbUpdater(IStockRepository stocks)
        {
            this.stocks = stocks;
        }

        /// <summary>
        /// Do Update.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Update()
        {
            var result = await NasdaqData.NasdaqService.GetSymbolListAsync().ConfigureAwait(false);

            if (result == null || !result.Items.Any())
            {
                return;
            }

            foreach (var item in result.Items)
            {
                bool found = await this.stocks.ExistsBySymbolAsync(item.Symbol).ConfigureAwait(false);

                if (!found)
                {
                    Console.WriteLine($"{item.Symbol} = {item.Name}");
                    await this.stocks.AddAsync(new Stock()
                    {
                        Symbol = item.Symbol,
                        Name = item.Name,
                    }).ConfigureAwait(false);
                }
            }
        }
    }
}