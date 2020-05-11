// <copyright file="Program.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader
{
    using System.Threading.Tasks;
    using LiteDbLoader.DbUpdater;
    using LiteDbLoader.Model;

    /// <summary>
    /// Main program class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Start of main program.
        /// </summary>
        /// <returns>Task.</returns>
        private static async Task Main()
        {
            using LiteDbRepository db = new LiteDbRepository(@"c:\code\Data.LiteDB");

            var updater = new NasdaqDbUpdater(db.Stocks);
            await updater.Update().ConfigureAwait(false);
        }
    }
}