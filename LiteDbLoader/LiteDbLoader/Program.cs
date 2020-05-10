// <copyright file="Program.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader
{
    using System.Threading.Tasks;

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
            await NasdaqData.NasdaqService.GetSymbolListAsync().ConfigureAwait(false);
        }
    }
}