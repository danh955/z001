// <copyright file="Program.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace CsvParsing
{
    using System;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Main program class.
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            string url = "ftp://ftp.nasdaqtrader.com/symboldirectory/nasdaqlisted.txt";
            WebClient client = new WebClient();
            string text = client.DownloadString(url);

            bool firstLine = true;
            foreach (string[] columns in text.GetCSVs('|'))
            {
                if (columns.Count() > 0)
                {
                    if (firstLine)
                    {
                        // Header line.
                        Console.WriteLine(string.Join(", ", columns));
                        Console.WriteLine();
                        firstLine = false;
                    }
                    else
                    {
                        const string CompareText = "File Creation Time:";

                        if (columns[0].StartsWith(CompareText))
                        {
                            string dateText = columns[0].Substring(CompareText.Length).Trim();

                            // MMDDYYYHHMM
                            int month = int.Parse(dateText.Substring(0, 2));
                            int day = int.Parse(dateText.Substring(2, 2));
                            int year = int.Parse(dateText.Substring(4, 4));
                            int hour = int.Parse(dateText.Substring(8, 2));
                            //// It may have a colon between the hour and minute.
                            int minute = int.Parse(dateText.Substring(dateText[10] == ':' ? 11 : 10, 2));
                            DateTime fileCreationTime = new DateTime(year, month, day, hour, minute, 0);
                            Console.WriteLine(fileCreationTime);
                        }
                        else
                        {
                            // process the line
                            Console.WriteLine(string.Join(", ", columns));
                        }
                    }
                }
            }
        }
    }
}