// <copyright file="NasdaqService.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader.NasdaqData
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// NASDAQ service class.
    /// </summary>
    public static class NasdaqService
    {
        /// <summary>
        /// Get the list of symbols.
        /// </summary>
        /// <returns>List of symbols.</returns>
        public static async Task<NasdaqResult<NasdaqSymbols>> GetSymbolListAsync()
        {
            const string FileCreationTimeText = "File Creation Time:";
            const string NasdaqListedFileUri = @"ftp://ftp.nasdaqtrader.com/symboldirectory/nasdaqlisted.txt";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(NasdaqListedFileUri));
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            bool firstLine = true;
            DateTime? fileCreationTime = null;
            List<NasdaqSymbols> data = new List<NasdaqSymbols>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var columns = line.Split('|');

                    if (firstLine)
                    {
                        // Header line.
                        firstLine = false;
                    }
                    else
                    {
                        if (columns[0].StartsWith(FileCreationTimeText, StringComparison.OrdinalIgnoreCase))
                        {
                            string dateText = columns[0].Substring(FileCreationTimeText.Length).Trim();

                            // MMDDYYYHHMM
                            int month = int.Parse(dateText.Substring(0, 2), CultureInfo.InvariantCulture);
                            int day = int.Parse(dateText.Substring(2, 2), CultureInfo.InvariantCulture);
                            int year = int.Parse(dateText.Substring(4, 4), CultureInfo.InvariantCulture);
                            int hour = int.Parse(dateText.Substring(8, 2), CultureInfo.InvariantCulture);
                            //// It may have a colon between the hour and minute.
                            int minute = int.Parse(dateText.Substring(dateText[10] == ':' ? 11 : 10, 2), CultureInfo.InvariantCulture);
                            fileCreationTime = new DateTime(year, month, day, hour, minute, 0);
                        }
                        else
                        {
                            if (columns.Length > 1)
                            {
                                data.Add(new NasdaqSymbols
                                {
                                    Symbol = columns[0],
                                    Name = columns[1],
                                });
                            }
                        }
                    }
                }
            }

            return new NasdaqResult<NasdaqSymbols>(data, fileCreationTime);
        }
    }
}